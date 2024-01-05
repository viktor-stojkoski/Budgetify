import { Inject, Injectable, OnDestroy } from '@angular/core';
import { MSAL_GUARD_CONFIG, MsalBroadcastService, MsalGuardConfiguration, MsalService } from '@azure/msal-angular';
import {
  AccountInfo,
  AuthenticationResult,
  EventMessage,
  EventType,
  InteractionStatus,
  RedirectRequest,
  SilentRequest,
  SsoSilentRequest
} from '@azure/msal-browser';
import { IdTokenClaims, PromptValue } from '@azure/msal-common';
import { SnackbarService } from '@budgetify/shared';
import { Observable, Subject, filter, takeUntil } from 'rxjs';
import { environment } from '../../../environments/environment';
import { b2cPolicies } from '../configs/auth.config';
import { CurrentUser } from '../models/auth.model';

type IdTokenClaimsWithPolicyId = IdTokenClaims & {
  acr?: string;
  tfp?: string;
};

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private _currentUser$: Subject<CurrentUser | null>;
  private readonly _destroying$ = new Subject<void>();

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private msalService: MsalService,
    private msalBroadcastService: MsalBroadcastService,
    private snackbarService: SnackbarService
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
        error: (error) => this.snackbarService.showError(error)
      });

    this.msalBroadcastService.msalSubject$
      .pipe(
        filter(
          (msg: EventMessage) =>
            msg.eventType === EventType.LOGIN_SUCCESS ||
            msg.eventType === EventType.ACQUIRE_TOKEN_SUCCESS ||
            msg.eventType === EventType.SSO_SILENT_SUCCESS
        ),
        takeUntil(this._destroying$)
      )
      .subscribe({
        next: (result: EventMessage) => {
          let payload = result.payload as AuthenticationResult;
          let idToken = payload.idTokenClaims as IdTokenClaimsWithPolicyId;

          if (idToken.acr === b2cPolicies.names.signUpSignIn || idToken.tfp === b2cPolicies.names.signUpSignIn) {
            this.msalService.instance.setActiveAccount(payload.account);
          }

          if (idToken.acr === b2cPolicies.names.editProfile || idToken.tfp === b2cPolicies.names.editProfile) {
            const originalSignInAccount = this.msalService.instance
              .getAllAccounts()
              .find(
                (account: AccountInfo) =>
                  account.idTokenClaims?.oid === idToken.oid &&
                  account.idTokenClaims?.sub === idToken.sub &&
                  ((account.idTokenClaims as IdTokenClaimsWithPolicyId).acr === b2cPolicies.names.signUpSignIn ||
                    (account.idTokenClaims as IdTokenClaimsWithPolicyId).tfp === b2cPolicies.names.signUpSignIn)
              );
            let signUpSignInFlowRequest: SsoSilentRequest = {
              authority: b2cPolicies.authorities.signUpSignIn,
              account: originalSignInAccount
            };
            this.msalService.ssoSilent(signUpSignInFlowRequest);
          }

          if (idToken.acr === b2cPolicies.names.resetPassword || idToken.tfp === b2cPolicies.names.resetPassword) {
            let signUpSignInFlowRequest: RedirectRequest = {
              authority: b2cPolicies.authorities.signUpSignIn,
              prompt: PromptValue.LOGIN,
              scopes: ['openid']
            };

            this.login(signUpSignInFlowRequest);
          }

          return result;
        }
      });

    this.msalBroadcastService.msalSubject$
      .pipe(
        filter(
          (msg: EventMessage) =>
            msg.eventType === EventType.LOGIN_FAILURE || msg.eventType === EventType.ACQUIRE_TOKEN_FAILURE
        ),
        takeUntil(this._destroying$)
      )
      .subscribe({
        next: (result: EventMessage) => {
          if (result.error && result.error.message.indexOf('AADB2C90118') > -1) {
            let resetPasswordFlowRequest: RedirectRequest = {
              authority: b2cPolicies.authorities.resetPassword,
              scopes: ['openid']
            };

            this.login(resetPasswordFlowRequest);
          }
        }
      });
  }

  get currentUser(): Observable<CurrentUser | null> {
    return this._currentUser$.asObservable();
  }

  public ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }

  public login(userFlowRequest?: RedirectRequest): void {
    if (this.msalGuardConfig.authRequest) {
      this.msalService.loginRedirect({ ...this.msalGuardConfig.authRequest, ...userFlowRequest } as RedirectRequest);
    } else {
      this.msalService.loginRedirect(userFlowRequest);
    }
  }

  public logout(): void {
    this.msalService.logoutRedirect();
    sessionStorage.removeItem('msal.interaction.status');
  }

  public isAuthenticated(): boolean {
    return this.msalService.instance.getAllAccounts().length > 0;
  }

  public getSelfUser(): CurrentUser | null {
    if (this.isAuthenticated()) {
      return this.getClaims(this.msalService.instance.getActiveAccount()?.idTokenClaims);
    }
    return null;
  }

  public acquireTokenSilent(): void {
    const originalSignInAccount = this.msalService.instance
      .getAllAccounts()
      .find(
        (account: AccountInfo) =>
          (account.idTokenClaims as IdTokenClaimsWithPolicyId).acr === b2cPolicies.names.signUpSignIn ||
          (account.idTokenClaims as IdTokenClaimsWithPolicyId).tfp === b2cPolicies.names.signUpSignIn
      );

    var request: SilentRequest = {
      scopes: ['openid'],
      account: originalSignInAccount,
      forceRefresh: true
    };

    this.msalService.acquireTokenSilent(request).subscribe({
      next: (result) => {
        this.msalService.instance.setActiveAccount(result.account);
      }
    });
  }

  // Alternative way to open Edit Profile though without re-login
  // Though it will not update the UI with the changes
  public getEditProfileRoute(): string {
    const url = new URL(`
      https://${environment.azureADB2C.tenantName}.b2clogin.com/${environment.azureADB2C.tenantName}.onmicrosoft.com/oauth2/v2.0/authorize`);

    url.searchParams.append('p', b2cPolicies.names.editProfile);
    url.searchParams.append('client_id', environment.azureADB2C.clientId);
    url.searchParams.append('nonce', 'defaultNonce');
    url.searchParams.append('redirect_uri', environment.azureADB2C.redirectUrl);
    url.searchParams.append('scope', 'openid');
    url.searchParams.append('response_type', 'id_token');

    return url.href;
  }

  // Prompts Re-Login
  public editProfile(): void {
    let editProfileFlowRequest: RedirectRequest = {
      authority: b2cPolicies.authorities.editProfile,
      scopes: ['openid']
    };

    this.login(editProfileFlowRequest);
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
      this.msalService.instance.setActiveAccount(accounts[0]);
    }

    if (activeAccount) {
      this._currentUser$.next(this.getClaims(activeAccount.idTokenClaims));
    } else {
      this._currentUser$.next(null);
    }
  }
}
