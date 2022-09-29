import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'translateEnum'
})
export class TranslateEnumPipe implements PipeTransform {
  constructor(private translateService: TranslateService) {}

  public transform(value: string, enumType: { [x: string]: string }): string {
    return this.translateService.instant(enumType[value]);
  }
}
