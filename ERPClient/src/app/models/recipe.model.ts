import { ProductModel } from "./product.model";
import { RecipeDetailModel } from "./recipe-detail.model";

export class RecipeModel{
    id: string = "";
    productId: string = "";
    product: ProductModel = new ProductModel();
    details: RecipeDetailModel[] = [];
}