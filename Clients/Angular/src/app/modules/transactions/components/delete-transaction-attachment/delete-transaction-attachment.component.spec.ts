import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppModule } from '../../../../app.module';
import { TransactionsModule } from '../../transactions.module';

import { DeleteTransactionAttachmentComponent } from './delete-transaction-attachment.component';

describe('DeleteTransactionAttachmentComponent', () => {
  let component: DeleteTransactionAttachmentComponent;
  let fixture: ComponentFixture<DeleteTransactionAttachmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransactionsModule, AppModule],
      declarations: [DeleteTransactionAttachmentComponent],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }]
    }).compileComponents();

    fixture = TestBed.createComponent(DeleteTransactionAttachmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
