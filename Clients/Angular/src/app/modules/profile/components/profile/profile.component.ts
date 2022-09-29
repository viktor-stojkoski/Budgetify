import { Component, OnInit } from '@angular/core';
import { AuthService, CurrentUser } from '@budgetify/core';
import { DestroyBaseComponent } from '@budgetify/shared';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent extends DestroyBaseComponent implements OnInit {
  public user: CurrentUser | null | undefined;

  constructor(private authService: AuthService) {
    super();
  }

  public ngOnInit(): void {
    this.getProfile();
  }

  public getProfile(): void {
    this.user = this.authService.getSelfUser();
  }
}
