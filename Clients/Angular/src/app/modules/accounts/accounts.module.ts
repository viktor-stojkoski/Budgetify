import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@budgetify/core';
import { DialogService, SharedModule } from '@budgetify/shared';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { AccountsTableComponent } from './components/accounts-table/accounts-table.component';
import { CreateAccountComponent } from './components/create-account/create-account.component';
import { routes } from './routes';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    CoreModule,
    SharedModule,
    MatTableModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatAutocompleteModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatCardModule
  ],
  declarations: [AccountsTableComponent, CreateAccountComponent, AccountDetailsComponent, DeleteAccountComponent],
  exports: [AccountsTableComponent],
  providers: [DialogService]
})
export class AccountsModule {}
