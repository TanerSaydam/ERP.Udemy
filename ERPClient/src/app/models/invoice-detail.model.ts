import { DepotModel } from "./depot.model";
import { ProductModel } from "./product.model";

export class InvoiceDetailModel{
    id: string = "";
    InvoiceId: string = "";
    productId: string = "";
    depotId: string = "";
    depot: DepotModel = new DepotModel();
    product:ProductModel = new ProductModel();
    quantity:number = 0;
    price: number = 0;    
}