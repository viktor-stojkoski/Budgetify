import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@budgetify/core';
import { AccountsTableComponent } from './components/accounts-table/accounts-table.component';
import { routes } from './routes';

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes), CoreModule, MatTableModule],
  declarations: [AccountsTableComponent],
  exports: [AccountsTableComponent]
})
export class AccountsModule {}
