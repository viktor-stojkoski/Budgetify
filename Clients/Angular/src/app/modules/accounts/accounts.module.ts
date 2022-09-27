import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@budgetify/core';
import { AccountsTableComponent } from './components/accounts-table/accounts-table.component';
import { CreateAccountComponent } from './components/create-account/create-account.component';
import { routes } from './routes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    CoreModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule
  ],
  declarations: [AccountsTableComponent, CreateAccountComponent],
  exports: [AccountsTableComponent]
})
export class AccountsModule {}
