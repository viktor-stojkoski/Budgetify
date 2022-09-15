import { Component, OnInit } from '@angular/core';
import { CurrentUser } from 'src/app/models/auth.models';
import { AuthService } from 'src/app/services/auth.service';

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
