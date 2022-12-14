import { Route } from '@angular/router';
import { ExchangeRateDetailsComponent } from './components/exchange-rate-details/exchange-rate-details.component';
import { ExchangeRatesTableComponent } from './components/exchange-rates-table/exchange-rates-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: ExchangeRatesTableComponent
  },
  {
    path: ':exchangeRateUid',
    component: ExchangeRateDetailsComponent
  }
];
