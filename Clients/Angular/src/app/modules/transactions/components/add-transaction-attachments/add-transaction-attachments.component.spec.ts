import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppModule } from '../../../../app.module';
import { TransactionsModule } from '../../transactions.module';

import { AddTransactionAttachmentsComponent } from './add-transaction-attachments.component';

describe('AddTransactionAttachmentsComponent', () => {
  let component: AddTransactionAttachmentsComponent;
  let fixture: ComponentFixture<AddTransactionAttachmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransactionsModule, AppModule],
      declarations: [AddTransactionAttachmentsComponent],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }]
    }).compileComponents();

    fixture = TestBed.createComponent(AddTransactionAttachmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
