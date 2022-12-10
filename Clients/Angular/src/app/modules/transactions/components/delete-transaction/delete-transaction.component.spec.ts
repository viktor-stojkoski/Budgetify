import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppModule } from '../../../../app.module';
import { TransactionsModule } from '../../transactions.module';

import { DeleteTransactionComponent } from './delete-transaction.component';

describe('DeleteTransactionComponent', () => {
  let component: DeleteTransactionComponent;
  let fixture: ComponentFixture<DeleteTransactionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransactionsModule, AppModule],
      declarations: [DeleteTransactionComponent],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }]
    }).compileComponents();

    fixture = TestBed.createComponent(DeleteTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
