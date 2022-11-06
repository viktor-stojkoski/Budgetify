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
import { IDeleteMerchantDialogData } from '../../models/merchant.model';
import { MerchantService } from '../../services/merchant.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-delete-merchant',
  templateUrl: './delete-merchant.component.html',
  styleUrls: ['./delete-merchant.component.scss']
})
export class DeleteMerchantComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;

  constructor(
    @Inject(MAT_DIALOG_DATA) public merchant: IDeleteMerchantDialogData,
    private dialogRef: MatDialogRef<DeleteMerchantComponent>,
    private merchantService: MerchantService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public deleteMerchant(): void {
    this.merchantService
      .deleteMerchant(this.merchant.uid)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
          this.snackbarService.success(this.translationKeys.deleteMerchantSuccessful);
        },
        error: (error) => this.snackbarService.showError(error)
      });
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }
}
