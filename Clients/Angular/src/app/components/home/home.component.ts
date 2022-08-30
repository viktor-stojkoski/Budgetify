import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public displayLogin = false;

  constructor(private authService: AuthService) {}

  public ngOnInit(): void {
    this.isLoggedIn();
    this.authService.selfUser.subscribe({
      next: () => this.isLoggedIn()
    });
  }

  public login(): void {
    this.authService.login();
    this.isLoggedIn();
  }

  public isLoggedIn(): void {
    this.displayLogin = this.authService.isLoggedIn();
  }

  public logout(): void {
    this.authService.logout();
  }
}
