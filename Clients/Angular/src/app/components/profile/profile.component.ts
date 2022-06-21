import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public profile?: {
    name?: string,
    city?: string,
    surname?: string
  } | null;

  constructor(private authService: AuthService) { }

  public ngOnInit(): void {
    this.getProfile();
  }

  public getProfile(): void {
    if (this.authService.getSelfUser() !== null) {
      this.profile = this.authService.getSelfUser();
    }
  }
}
