import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteMerchantComponent } from './delete-merchant.component';

describe('DeleteMerchantComponent', () => {
  let component: DeleteMerchantComponent;
  let fixture: ComponentFixture<DeleteMerchantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteMerchantComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteMerchantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
