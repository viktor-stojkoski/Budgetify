import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProfileComponent } from './components/profile/profile.component';
import { routes } from './routes';

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  declarations: [ProfileComponent],
  exports: [ProfileComponent]
})
export class ProfileModule {}
