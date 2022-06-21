import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public isLoggedIn = false;

  constructor(private authService: AuthService) { }

  public ngOnInit(): void {
    this.checkedLoggedIn();
  }

  public login(): void {
    this.authService.login();
  }

  public checkedLoggedIn(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  public logout(): void {
    this.authService.logout();
  }
}
