import { Route } from '@angular/router';
import { TransactionsTableComponent } from './components/transactions-table/transactions-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: TransactionsTableComponent
  }
];
