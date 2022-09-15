import { Component, OnInit } from '@angular/core';
import { CurrentUser } from 'src/app/models/auth.models';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public isAuthenticated: boolean | undefined;
  public currentUser: CurrentUser | null | undefined;

  constructor(private authService: AuthService) {}

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
}
