import { Pipe, PipeTransform } from '@angular/core';
import { OrderModel } from '../models/order.model';

@Pipe({
  name: 'order',
  standalone: true
})
export class OrderPipe implements PipeTransform {

  transform(value: OrderModel[], search:string): OrderModel[] {
    if(!search){
      return value;
    }

    return value.filter(p=> 
      p.orderNumber.toString().includes(search) ||
      p.orderNumberYear.toString().includes(search) ||
      p.customer.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.date.includes(search) ||
      p.deliveryDate.includes(search) ||
      p.status.name.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
