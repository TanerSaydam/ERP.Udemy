import { Component, ElementRef, ViewChild } from '@angular/core';
import { InvoiceModel } from '../../models/invoice.model';
import { CustomerModel } from '../../models/customer.model';
import { ProductModel } from '../../models/product.model';
import { InvoiceDetailModel } from '../../models/invoice-detail.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { DatePipe } from '@angular/common';
import { DepotModel } from '../../models/depot.model';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SharedModule } from '../../modules/shared.module';
import { InvoicePipe } from '../../pipes/invoice.pipe';
import { OrderModel } from '../../models/order.model';

@Component({
  selector: 'app-invoices',
  standalone: true,
  imports: [SharedModule, InvoicePipe],
  providers: [DatePipe],
  templateUrl: './invoices.component.html',
  styleUrl: './invoices.component.css'
})
export class InvoicesComponent {
  invoices: InvoiceModel[] = [];
  customers: CustomerModel[] = [];
  products: ProductModel[] = [];
  depots: DepotModel[] = [];
  createDetail: InvoiceDetailModel = new InvoiceDetailModel();
  updateDetail: InvoiceDetailModel = new InvoiceDetailModel();
  search:string = "";
  type: number = 1;
  typeName: string = "Alış";
  orders: OrderModel[] = [];
  customerOrders: OrderModel[] = [];

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:InvoiceModel = new InvoiceModel();
  updateModel:InvoiceModel = new InvoiceModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private date: DatePipe,
    private activated: ActivatedRoute
  ){
    this.activated.params.subscribe(res=> {
      this.type = res["type"] == "purchase" ? 1 : 2;
      console.log(res);
      
      this.typeName = this.type == 1 ? "Alış" : "Satış";

      this.createModel.date = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.createModel.typeValue = this.type;

      this.getAll();
      this.getAllProducts();
      this.getAllCustomers();
      this.getAllDepots();
      this.getAllOrders();
    })    
  }

  getAll(){
    this.http.post<InvoiceModel[]>("Invoices/GetAll",{type: this.type},(res)=> {
      this.invoices = res;
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

  getAllDepots(){
    this.http.post<DepotModel[]>("Depots/GetAll",{},(res)=> {
      this.depots = res;
    });
  }

  getAllOrders(){
    this.http.post<OrderModel[]>("Orders/GetAll",{},(res)=> {
      this.orders = res.filter(p=> p.status.value < 3);
    });
  }

  addDetail(){
    const product = this.products.find(p=> p.id == this.createDetail.productId);
    if(product){
      this.createDetail.product = product;
    }

    const depot = this.depots.find(p=> p.id == this.createDetail.depotId);
    if(depot){
      this.createDetail.depot = depot;
    }

    this.createModel.details.push(this.createDetail);
    this.createDetail = new InvoiceDetailModel();
  }

  addUpdateDetail(){
    const product = this.products.find(p=> p.id == this.updateDetail.productId);
    if(product){
      this.updateDetail.product = product;
    }

    const depot = this.depots.find(p=> p.id == this.updateDetail.depotId);
    if(depot){
      this.updateDetail.depot = depot;
    }

    this.updateModel.details.push(this.updateDetail);
    this.updateDetail = new InvoiceDetailModel();
  }

  removeDetail(index:number){
    this.createModel.details.splice(index,1);
  }

  removeUpdateDetail(index:number){
    this.updateModel.details.splice(index,1);
  }

  create(form: NgForm){
    if(form.valid){
      this.http.post<string>("Invoices/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new InvoiceModel();
        this.createModel.date = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
        this.createModel.typeValue = this.type;
        
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: InvoiceModel){    
    this.swal.callSwal("Faturayı Sil?",`${model.customer.name} - ${model.invoiceNumber} numaralı faturayı silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Invoices/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: InvoiceModel){
    this.updateModel = {...model};
  }

  update(form: NgForm){
    if(form.valid){
      this.http.post<string>("Invoices/Update",this.updateModel,(res)=> {
        this.swal.callToast(res,"info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  setSelectedCustomerOrders(){
    this.customerOrders = this.orders.filter(p=> p.customerId == this.createModel.customerId);
  }
}
