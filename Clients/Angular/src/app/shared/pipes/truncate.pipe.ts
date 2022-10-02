import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe implements PipeTransform {
  public transform(value: string, length: number = 50, suffix: string = '...'): string {
    return value.length < length ? value : value.slice(0, length) + suffix;
  }
}
