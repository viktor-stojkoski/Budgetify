import { Route } from '@angular/router';
import { TransactionDetailsComponent } from './components/transaction-details/transaction-details.component';
import { TransactionsTableComponent } from './components/transactions-table/transactions-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: TransactionsTableComponent
  },
  {
    path: ':transactionUid',
    component: TransactionDetailsComponent
  }
];
