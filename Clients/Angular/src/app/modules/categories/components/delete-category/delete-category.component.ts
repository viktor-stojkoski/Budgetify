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
import { IDeleteCategoryDialogData } from '../../models/category.model';
import { CategoryService } from '../../services/category.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-delete-category',
  templateUrl: './delete-category.component.html',
  styleUrls: ['./delete-category.component.scss']
})
export class DeleteCategoryComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;

  constructor(
    @Inject(MAT_DIALOG_DATA) public category: IDeleteCategoryDialogData,
    private dialogRef: MatDialogRef<DeleteCategoryComponent>,
    private categoryService: CategoryService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public deleteCategory(): void {
    this.categoryService
      .deleteCategory(this.category.uid)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
          this.snackbarService.success(this.translationKeys.deleteCategorySuccessful);
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
