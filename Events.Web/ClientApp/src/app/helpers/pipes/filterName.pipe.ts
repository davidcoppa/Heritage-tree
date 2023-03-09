import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterName'
})
export class FilterNamePipe implements PipeTransform {
  transform(items: any[]): string {
    console.log("asdasdas "+items)
    // return empty array if array is falsy
    if (!items) { return ""; }

    var ret = '';
    for (var i = 0; i < items.length; i++) {
      
      ret+=items[i].name +", "
    }
    return ret;
  
  }
}
