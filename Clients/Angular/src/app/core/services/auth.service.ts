import { Inject, Injectable, OnDestroy } from '@angular/core';
import { MsalBroadcastService, MsalGuardConfiguration, MsalService, MSAL_GUARD_CONFIG } from '@azure/msal-angular';
import { InteractionStatus, RedirectRequest } from '@azure/msal-browser';
import { filter, Observable, Subject, takeUntil } from 'rxjs';
import { CurrentUser } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private _currentUser$: Subject<CurrentUser | null>;
  private readonly _destroying$ = new Subject<void>();

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private msalService: MsalService,
    private msalBroadcastService: MsalBroadcastService
  ) {
    this._currentUser$ = new Subject();
    this._destroying$ = new Subject<void>();
    this.msalBroadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None),
        takeUntil(this._destroying$)
      )
      .subscribe({
        next: () => this.refreshAuthUser(),
        error: (error) => console.error(error)
      });
  }

  get currentUser(): Observable<CurrentUser | null> {
    return this._currentUser$.asObservable();
  }

  public ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }

  public login(): void {
    if (this.msalGuardConfig.authRequest) {
      this.msalService.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
    } else {
      this.msalService.loginRedirect();
    }
  }

  public logout(): void {
    this.msalService.logoutRedirect();
    sessionStorage.removeItem('msal.interaction.status');
  }

  public isLoggedIn(): boolean {
    return this.msalService.instance.getActiveAccount() !== null;
  }

  public getSelfUser(): CurrentUser | null {
    if (this.isLoggedIn()) {
      return this.getClaims(this.msalService.instance.getActiveAccount()?.idTokenClaims);
    }
    return null;
  }

  private getClaims(claims?: { [key: string]: any }): CurrentUser | null {
    if (claims) {
      return {
        email: claims['emails'][0] as string,
        city: claims['city'] as string,
        firstName: claims['given_name'] as string,
        lastName: claims['family_name'] as string
      };
    }

    return null;
  }

  private refreshAuthUser(): void {
    let activeAccount = this.msalService.instance.getActiveAccount();

    if (!activeAccount && this.msalService.instance.getAllAccounts().length > 0) {
      let accounts = this.msalService.instance.getAllAccounts();
      activeAccount = accounts[0];
      this.msalService.instance.setActiveAccount(activeAccount);
    }

    if (activeAccount) {
      this._currentUser$.next(this.getClaims(activeAccount.idTokenClaims));
    } else {
      this._currentUser$.next(null);
    }
  }
}
