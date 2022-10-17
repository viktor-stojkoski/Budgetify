import { TestBed } from '@angular/core/testing';
import { AppModule } from '../../app.module';
import { CoreModule } from '../core.module';

import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [CoreModule, AppModule]
    });
    service = TestBed.inject(AuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
