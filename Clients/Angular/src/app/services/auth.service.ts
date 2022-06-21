import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { SelfUser } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private msalService: MsalService) { }

  public login(): void {
    this.msalService.loginPopup().subscribe({
      next: response => {
        this.msalService.instance.setActiveAccount(response.account);
      },
      error: err => {
        console.error(err);
      }
    });
  }

  public logout(): void {
    this.msalService.logout();
  }

  public isLoggedIn(): boolean {
    return this.msalService.instance.getActiveAccount() !== null;
  }

  public getSelfUser(): SelfUser | null {
    if (this.isLoggedIn()) {
      return this.getClaims(this.msalService.instance.getActiveAccount()?.idTokenClaims);
    }
    return null;
  }

  private getClaims(claims?: { [key: string]: unknown }): SelfUser | null {
    if (claims) {
      return {
        city: claims['city'] as string,
        name: claims['given_name'] as string,
        surname: claims['family_name'] as string
      }
    }

    return null;
  }
}
