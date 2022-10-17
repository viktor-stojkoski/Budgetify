import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DestroyBaseComponent, SnackbarService, TranslationKeys as SharedTranslationKeys } from '@budgetify/shared';
import { take } from 'rxjs';
import { IDeleteAccountDialogData } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-delete-account',
  templateUrl: './delete-account.component.html',
  styleUrls: ['./delete-account.component.scss']
})
export class DeleteAccountComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;

  constructor(
    @Inject(MAT_DIALOG_DATA) public account: IDeleteAccountDialogData,
    private dialogRef: MatDialogRef<DeleteAccountComponent>,
    private accountService: AccountService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public deleteAccount(): void {
    this.accountService
      .deleteAccount(this.account.uid)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.dialogRef.close();
          this.snackbarService.success(this.translationKeys.deleteAccountSuccessful);
        },
        error: (error) => this.snackbarService.showError(error)
      });
  }

  public onCancelClick(): void {
    this.dialogRef.close();
  }
}
