import { Component, OnInit } from '@angular/core';
import { AuthService, CurrentUser } from '@budgetify/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public user: CurrentUser | null | undefined;

  constructor(private authService: AuthService) {}

  public ngOnInit(): void {
    this.getProfile();
  }

  public getProfile(): void {
    this.user = this.authService.getSelfUser();
  }
}
