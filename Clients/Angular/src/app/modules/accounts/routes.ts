import { Route } from '@angular/router';
import { AccountsTableComponent } from './components/accounts-table/accounts-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: AccountsTableComponent
  }
];
