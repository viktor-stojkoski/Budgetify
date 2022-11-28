import { Route } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from '@budgetify/core';

export const routes: Route[] = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'profile',
    canActivateChild: [MsalGuard],
    loadChildren: () => import('./modules/profile/profile.module').then((m) => m.ProfileModule)
  },
  {
    path: 'accounts',
    canActivateChild: [MsalGuard],
    loadChildren: () => import('./modules/accounts/accounts.module').then((m) => m.AccountsModule)
  },
  {
    path: 'categories',
    canActivateChild: [MsalGuard],
    loadChildren: () => import('./modules/categories/categories.module').then((m) => m.CategoriesModule)
  },
  {
    path: 'merchants',
    canActivateChild: [MsalGuard],
    loadChildren: () => import('./modules/merchants/merchants.module').then((m) => m.MerchantsModule)
  },
  {
    path: 'transactions',
    canActivateChild: [MsalGuard],
    loadChildren: () => import('./modules/transactions/transactions.module').then((m) => m.TransactionsModule)
  }
];
