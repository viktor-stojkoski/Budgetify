import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTransactionByScanComponent } from './create-transaction-by-scan.component';

describe('CreateTransactionByScanComponent', () => {
  let component: CreateTransactionByScanComponent;
  let fixture: ComponentFixture<CreateTransactionByScanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTransactionByScanComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateTransactionByScanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
