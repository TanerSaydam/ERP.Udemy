import { CustomerModel } from "./customer.model";
import { OrderDetailModel } from "./order-detail.model";

export class OrderModel{
    id: string = "";
    orderNumber: number = 0;
    orderNumberYear : number = 0;
    number:string = "";
    date: string = "";
    deliveryDate: string = "";
    customerId: string = "";
    customer: CustomerModel = new CustomerModel();
    status: OrderStatusEnum = new OrderStatusEnum();
    details: OrderDetailModel[] = [];
}

export class OrderStatusEnum{
    value: number = 1;
    name: string  ="";
}