import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { take } from 'rxjs';
import { IDeleteTransactionAttachmentDialogData } from '../../models/transaction.model';
import { TransactionService } from '../../services/transaction.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-delete-transaction-attachment',
  templateUrl: './delete-transaction-attachment.component.html',
  styleUrls: ['./delete-transaction-attachment.component.scss']
})
export class DeleteTransactionAttachmentComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;

  constructor(
    @Inject(MAT_DIALOG_DATA) public attachment: IDeleteTransactionAttachmentDialogData,
    private dialogRef: MatDialogRef<DeleteTransactionAttachmentComponent>,
    private transactionService: TransactionService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public deleteTransactionAttachment(): void {
    this.transactionService
      .deleteTransactionAttachment(this.attachment.transactionUid, this.attachment.attachmentUid)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
          this.snackbarService.success(this.translationKeys.deleteTransactionAttachmentSuccessful);
        },
        error: (error) => {
          this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
          this.snackbarService.showError(error);
        }
      });
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }
}
