import { TestBed } from '@angular/core/testing';
import { MerchantsModule } from '../merchants.module';

import { MerchantService } from './merchant.service';

describe('MerchantService', () => {
  let service: MerchantService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [MerchantsModule]
    });
    service = TestBed.inject(MerchantService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
