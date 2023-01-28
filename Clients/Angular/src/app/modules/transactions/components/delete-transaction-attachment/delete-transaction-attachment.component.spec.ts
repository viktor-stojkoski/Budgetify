import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteTransactionAttachmentComponent } from './delete-transaction-attachment.component';

describe('DeleteTransactionAttachmentComponent', () => {
  let component: DeleteTransactionAttachmentComponent;
  let fixture: ComponentFixture<DeleteTransactionAttachmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteTransactionAttachmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteTransactionAttachmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
