import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'translateEnum',
  pure: false
})
export class TranslateEnumPipe implements PipeTransform {
  private translatedValue: string = '';

  constructor(private translateService: TranslateService) {}

  public transform(value: string, enumType: { [x: string]: string }): string {
    if (this.translateService.instant(enumType[value]) === this.translatedValue) {
      return this.translatedValue;
    }

    this.translatedValue = this.translateService.instant(enumType[value]);

    return this.translatedValue;
  }
}
