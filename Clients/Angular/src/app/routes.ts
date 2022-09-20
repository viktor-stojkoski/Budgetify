import { Route } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './core/components/home/home.component';

export const routes: Route[] = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'profile',
    canActivateChild: [MsalGuard],
    loadChildren: () => import('./modules/profile/profile.module').then((m) => m.ProfileModule)
  }
];
