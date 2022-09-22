import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import {
  MsalBroadcastService,
  MsalGuard,
  MsalModule,
  MsalService,
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG
} from '@azure/msal-angular';
import { TranslateModule } from '@ngx-translate/core';
import { AuthInterceptor } from 'src/app/core/http/auth.interceptor';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MsalGuardConfigFactory, MsalInstanceFactory, MsalInterceptorConfigFactory } from './configs/auth-config';

@NgModule({
  declarations: [NavbarComponent, HomeComponent],
  imports: [
    CommonModule,
    MsalModule,
    RouterModule,
    TranslateModule,
    HttpClientModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatSelectModule,
    FlexLayoutModule,
    ReactiveFormsModule
  ],
  exports: [TranslateModule, NavbarComponent, HomeComponent],
  providers: [
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: MsalInterceptor,
    //   multi: true
    // },
    {
      provide: MSAL_INSTANCE,
      useFactory: MsalInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MsalGuardConfigFactory
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MsalInterceptorConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ]
})
export class CoreModule {}
