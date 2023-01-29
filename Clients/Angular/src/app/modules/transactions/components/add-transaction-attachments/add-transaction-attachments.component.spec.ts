import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTransactionAttachmentsComponent } from './add-transaction-attachments.component';

describe('AddTransactionAttachmentsComponent', () => {
  let component: AddTransactionAttachmentsComponent;
  let fixture: ComponentFixture<AddTransactionAttachmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTransactionAttachmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddTransactionAttachmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
