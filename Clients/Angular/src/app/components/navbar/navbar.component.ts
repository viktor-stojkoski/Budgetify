import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ILanguage } from 'src/app/models/interfaces';
import { CurrentUser } from '../../models/auth.models';
import { AuthService } from '../../services/auth.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
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

  constructor(private authService: AuthService, private translateService: TranslateService) {}

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
    this.isAuthenticated = this.authService.isLoggedIn();
    this.currentUser = this.authService.getSelfUser();
  }

  public logout(): void {
    this.authService.logout();
  }

  public changeLanguage() {
    this.translateService.use(this.selectedLanguage);
  }
}
