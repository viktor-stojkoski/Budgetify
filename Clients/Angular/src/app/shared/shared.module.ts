import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { TranslateEnumPipe } from './pipes/translate-enum.pipe';
import { TruncatePipe } from './pipes/truncate.pipe';

@NgModule({
  declarations: [TranslateEnumPipe, TruncatePipe],
  imports: [CommonModule, TranslateModule],
  exports: [TranslateEnumPipe, TruncatePipe]
})
export class SharedModule {}
