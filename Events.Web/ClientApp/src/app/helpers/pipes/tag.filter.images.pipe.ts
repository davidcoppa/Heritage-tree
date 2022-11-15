import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tagfilterimages'
})
export class FilterimagesPipe implements PipeTransform {
  transform(items: any[], tags: string[]): any {

    var retVal= new Array();
    for (var i = 0; i < tags.length; i++) {

      if (tags[i] == 'all' || tags[i] == '') {
        retVal = items;
        break;
      } else {
        retVal?.push(items.filter(item => { item.brand === tags }));
      }
    }
    return retVal;

    //if (tags === 'all') {
    //  return items;

    //} else {
    //  return items.filter(item => {
    //    return item.brand === tags;
    //  });
    //}
  }

}   
