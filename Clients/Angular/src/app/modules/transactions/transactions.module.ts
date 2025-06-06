import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
  MAT_NATIVE_DATE_FORMATS,
  MatDateFormats,
  MatNativeDateModule,
  NativeDateAdapter
} from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@budgetify/core';
import { DialogService, SharedModule } from '@budgetify/shared';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { AddTransactionAttachmentsComponent } from './components/add-transaction-attachments/add-transaction-attachments.component';
import { CreateTransactionByScanComponent } from './components/create-transaction-by-scan/create-transaction-by-scan.component';
import { CreateTransactionComponent } from './components/create-transaction/create-transaction.component';
import { DeleteTransactionAttachmentComponent } from './components/delete-transaction-attachment/delete-transaction-attachment.component';
import { DeleteTransactionComponent } from './components/delete-transaction/delete-transaction.component';
import { TransactionDetailsComponent } from './components/transaction-details/transaction-details.component';
import { TransactionsTableComponent } from './components/transactions-table/transactions-table.component';
import { routes } from './routes';

export const MY_DATE_FORMATS: MatDateFormats = {
  ...MAT_NATIVE_DATE_FORMATS,
  display: {
    ...MAT_NATIVE_DATE_FORMATS.display,
    dateInput: {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    } as Intl.DateTimeFormatOptions
  }
};

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    CoreModule,
    SharedModule,
    MatToolbarModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    NgxDropzoneModule,
    MatButtonToggleModule
  ],
  declarations: [
    TransactionsTableComponent,
    CreateTransactionComponent,
    TransactionDetailsComponent,
    DeleteTransactionComponent,
    DeleteTransactionAttachmentComponent,
    AddTransactionAttachmentsComponent,
    CreateTransactionByScanComponent
  ],
  providers: [
    DialogService,
    { provide: DateAdapter, useClass: NativeDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }
  ]
})
export class TransactionsModule {}
