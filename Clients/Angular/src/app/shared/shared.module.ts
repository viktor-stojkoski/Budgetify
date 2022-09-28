import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { TranslateEnumPipe } from './pipes/translate-enum.pipe';

@NgModule({
  declarations: [TranslateEnumPipe],
  imports: [CommonModule, TranslateModule],
  exports: [TranslateEnumPipe]
})
export class SharedModule {}
