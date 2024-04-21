import { Pipe, PipeTransform } from '@angular/core';
import { MenuModel } from '../menu';

@Pipe({
  name: 'menu',
  standalone: true
})
export class MenuPipe implements PipeTransform {

  transform(value: MenuModel[], search: string): MenuModel[] {
    if(search === ""){
      return value;
    }

    return value.filter(p=> p.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()));
  }
}
