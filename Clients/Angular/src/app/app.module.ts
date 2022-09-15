import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MsalBroadcastService,
  MsalGuard,
  MsalInterceptor,
  MsalModule,
  MsalRedirectComponent,
  MsalService,
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG
} from '@azure/msal-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProfileComponent } from './components/profile/profile.component';
import {
  MsalGuardConfigFactory,
  MsalInstanceFactory,
  MsalInterceptorConfigFactory
} from './services/configs/auth-config';

@NgModule({
  declarations: [AppComponent, HomeComponent, ProfileComponent, NavbarComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MsalModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    BrowserAnimationsModule,
    FlexLayoutModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
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
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule {}
