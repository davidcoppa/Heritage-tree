import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumPipe'
})
export class EnumPipe implements PipeTransform {
  transform(value: any): [number, string][] {
   
      const keys = Object.keys(value);

    return keys.slice(keys.length / 2).map(t => [value[t], t]);
  }
}