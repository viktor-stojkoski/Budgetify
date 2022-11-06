import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppModule } from '../../../../app.module';
import { MerchantsModule } from '../../merchants.module';

import { DeleteMerchantComponent } from './delete-merchant.component';

describe('DeleteMerchantComponent', () => {
  let component: DeleteMerchantComponent;
  let fixture: ComponentFixture<DeleteMerchantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MerchantsModule, AppModule],
      declarations: [DeleteMerchantComponent],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }]
    }).compileComponents();

    fixture = TestBed.createComponent(DeleteMerchantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
