import { Component, ElementRef, ViewChild } from '@angular/core';
import { RecipeModel } from '../../models/recipe.model';
import { ProductModel } from '../../models/product.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { SharedModule } from '../../modules/shared.module';
import { RecipePipe } from '../../pipes/recipe.pipe';
import { RecipeDetailModel } from '../../models/recipe-detail.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [SharedModule, RecipePipe, RouterLink],
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.css'
})
export class RecipesComponent {
  recipes: RecipeModel[] = [];
  search:string = "";
  products: ProductModel[] = [];
  semiProducts: ProductModel[] = [];

  detail: RecipeDetailModel = new RecipeDetailModel();

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:RecipeModel = new RecipeModel();
  updateModel:RecipeModel = new RecipeModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}

  ngOnInit(): void {
    this.getAll();
    this.getAllProducts();
  }

  getAll(){
    this.http.post<RecipeModel[]>("Recipes/GetAll",{},(res)=> {
      this.recipes = res;
    });
  }

  getAllProducts(){
    this.http.post<ProductModel[]>("Products/GetAll",{},(res)=> {
      this.products = res;
      this.semiProducts = res.filter(p=> p.type.value == 2);
    })
  }

  addDetail(){
    const product = this.products.find(p=> p.id == this.detail.productId);
    if(product){
      this.detail.product = product;
    }    

    this.createModel.details.push(this.detail);

    this.detail = new RecipeDetailModel();
  }

  removeDetail(index:number){
    this.createModel.details.splice(index,1);
  }

  create(form: NgForm){
    if(form.valid){
      this.http.post<string>("Recipes/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new RecipeModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: RecipeModel){
    this.swal.callSwal("Reçeteyi Sil?",`${model.product.name} ürüne ait reçeti silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Recipes/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }  
}
