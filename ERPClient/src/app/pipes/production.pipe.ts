import { Pipe, PipeTransform } from '@angular/core';
import { ProductionModel } from '../models/production.model';

@Pipe({
  name: 'production',
  standalone: true
})
export class ProductionPipe implements PipeTransform {

  transform(value: ProductionModel[], search:string): ProductionModel[] {
    if(!search){
      return value;
    }

    return value.filter(p=> 
      p.product.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.depot.name.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
