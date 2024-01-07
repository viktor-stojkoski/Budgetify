import { Component, OnInit } from '@angular/core';
import { DestroyBaseComponent, TranslationKeys } from '@budgetify/shared';
import { TranslateService } from '@ngx-translate/core';
import { CurrentUser } from '../../models/auth.model';
import { ILanguage } from '../../models/common.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent extends DestroyBaseComponent implements OnInit {
  public isAuthenticated: boolean | undefined;
  public currentUser: CurrentUser | null | undefined;
  public translationKeys = TranslationKeys;
  public languages: ILanguage[] = [
    {
      name: this.translationKeys.navbarLanguageEnglish,
      resource: 'en'
    },
    {
      name: this.translationKeys.navbarLanguageMacedonian,
      resource: 'mk'
    }
  ];
  public selectedLanguage: string = this.languages[0].resource;

  constructor(private authService: AuthService, private translateService: TranslateService) {
    super();
  }

  public ngOnInit(): void {
    this.isLoggedIn();
    this.authService.currentUser.subscribe({
      next: () => this.isLoggedIn()
    });
  }

  public login(): void {
    this.authService.login();
    this.isLoggedIn();
  }

  public isLoggedIn(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    this.currentUser = this.authService.getSelfUser();
  }

  public logout(): void {
    this.authService.logout();
  }

  public changeLanguage() {
    this.translateService.use(this.selectedLanguage);
  }

  public editProfile() {
    this.authService.editProfile();
  }
}
