import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';
import { TranslateEnumPipe } from './pipes/translate-enum.pipe';
import { TruncatePipe } from './pipes/truncate.pipe';

@NgModule({
  declarations: [TranslateEnumPipe, TruncatePipe],
  imports: [CommonModule, TranslateModule, MatDialogModule, MatSnackBarModule],
  exports: [TranslateEnumPipe, TruncatePipe, MatDialogModule],
  providers: [{ provide: MatDialogRef, useValue: {} }]
})
export class SharedModule {}
