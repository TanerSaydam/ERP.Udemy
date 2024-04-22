import { Pipe, PipeTransform } from '@angular/core';
import { ProductModel } from '../models/product.model';

@Pipe({
  name: 'product',
  standalone: true
})
export class ProductPipe implements PipeTransform {

  transform(value: ProductModel[], search:string): ProductModel[] {
    if(!search){
      return value;
    }

    return value.filter(p=> 
      p.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.type.name.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
