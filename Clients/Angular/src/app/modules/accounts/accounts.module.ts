import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@budgetify/core';
import { SharedModule } from '@budgetify/shared';
import { AccountsTableComponent } from './components/accounts-table/accounts-table.component';
import { CreateAccountComponent } from './components/create-account/create-account.component';
import { routes } from './routes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    CoreModule,
    SharedModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatInputModule,
    MatAutocompleteModule,
    MatTooltipModule
  ],
  declarations: [AccountsTableComponent, CreateAccountComponent],
  exports: [AccountsTableComponent],
  providers: [{ provide: MatDialogRef, useValue: {} }]
})
export class AccountsModule {}
