import { Route } from '@angular/router';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { AccountsTableComponent } from './components/accounts-table/accounts-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: AccountsTableComponent
  },
  {
    path: ':accountUid',
    component: AccountDetailsComponent
  }
];
