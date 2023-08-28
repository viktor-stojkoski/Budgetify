import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  TranslationKeys as SharedTranslationKeys,
  SnackbarService
} from '@budgetify/shared';
import { take } from 'rxjs';
import { IDeleteBudgetDialogData } from '../../models/budget.model';
import { BudgetService } from '../../services/budget.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-delete-budget',
  templateUrl: './delete-budget.component.html',
  styleUrls: ['./delete-budget.component.scss']
})
export class DeleteBudgetComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;

  constructor(
    @Inject(MAT_DIALOG_DATA) public budget: IDeleteBudgetDialogData,
    private dialogRef: MatDialogRef<DeleteBudgetComponent>,
    private budgetService: BudgetService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public deleteBudget(): void {
    this.budgetService
      .deleteBudget(this.budget.uid)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
          this.snackbarService.success(this.translationKeys.deleteBudgetSuccessful);
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
