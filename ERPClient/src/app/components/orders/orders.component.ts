import { Component, ElementRef, ViewChild } from '@angular/core';
import { OrderModel } from '../../models/order.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { SharedModule } from '../../modules/shared.module';
import { OrderPipe } from '../../pipes/order.pipe';
import { CustomerModel } from '../../models/customer.model';
import { ProductModel } from '../../models/product.model';
import { OrderDetailModel } from '../../models/order-detail.model';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [SharedModule, OrderPipe, RouterLink],
  providers: [DatePipe],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
  orders: OrderModel[] = [];
  customers: CustomerModel[] = [];
  products: ProductModel[] = [];
  createDetail: OrderDetailModel = new OrderDetailModel();
  updateDetail: OrderDetailModel = new OrderDetailModel();
  search:string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:OrderModel = new OrderModel();
  updateModel:OrderModel = new OrderModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private date: DatePipe
  ){
    this.createModel.date = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
    this.createModel.deliveryDate = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
  }

  ngOnInit(): void {
    this.getAll();
    this.getAllProducts();
    this.getAllCustomers();
  }

  getAll(){
    this.http.post<OrderModel[]>("Orders/GetAll",{},(res)=> {
      this.orders = res;
    });
  }

  getAllProducts(){
    this.http.post<ProductModel[]>("Products/GetAll",{},(res)=> {
      this.products = res;
    });
  }

  getAllCustomers(){
    this.http.post<CustomerModel[]>("Customers/GetAll",{},(res)=> {
      this.customers = res;
    });
  }

  addDetail(){
    const product = this.products.find(p=> p.id == this.createDetail.productId);
    if(product){
      this.createDetail.product = product;
    }

    this.createModel.details.push(this.createDetail);
    this.createDetail = new OrderDetailModel();
  }

  addUpdateDetail(){
    const product = this.products.find(p=> p.id == this.updateDetail.productId);
    if(product){
      this.updateDetail.product = product;
    }

    this.updateModel.details.push(this.updateDetail);
    this.updateDetail = new OrderDetailModel();
  }

  removeDetail(index:number){
    this.createModel.details.splice(index,1);
  }

  removeUpdateDetail(index:number){
    this.updateModel.details.splice(index,1);
  }

  create(form: NgForm){
    if(form.valid){
      this.http.post<string>("Orders/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new OrderModel();
        this.createModel.date = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
        this.createModel.deliveryDate = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";

        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: OrderModel){    
    this.swal.callSwal("Siparişi Sil?",`${model.customer.name} - ${model.number} numaralı siparişi silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Orders/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: OrderModel){
    this.updateModel = {...model};
  }

  update(form: NgForm){
    if(form.valid){
      this.http.post<string>("Orders/Update",this.updateModel,(res)=> {
        this.swal.callToast(res,"info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  setStatusClass(statusValue: number){
    if(statusValue === 1) return "text-danger";
    else if(statusValue === 2) return "text-primary";
    else return "text-success";
  }
}
