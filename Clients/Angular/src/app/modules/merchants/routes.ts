import { Route } from '@angular/router';
import { MerchantDetailsComponent } from './components/merchant-details/merchant-details.component';
import { MerchantsTableComponent } from './components/merchants-table/merchants-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: MerchantsTableComponent
  },
  {
    path: ':merchantUid',
    component: MerchantDetailsComponent
  }
];
