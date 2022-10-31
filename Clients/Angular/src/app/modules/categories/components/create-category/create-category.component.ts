import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  enumToTranslationEnum,
  IDialogResponseData,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { take } from 'rxjs';
import { CategoryType } from '../../models/category.enum';
import { CategoryService } from '../../services/category.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public types = enumToTranslationEnum(CategoryType);

  public categoryForm = this.formBuilder.group({
    name: ['', Validators.required],
    type: ['', Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<CreateCategoryComponent>,
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public createCategory(): void {
    if (this.categoryForm.valid) {
      this.categoryService
        .createCategory({
          name: this.categoryForm.controls.name.value,
          type: this.categoryForm.controls.type.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
            this.snackbarService.success(this.translationKeys.createCategorySuccessful);
          },
          error: (error) => this.snackbarService.showError(error)
        });
    } else {
      this.categoryForm.markAllAsTouched();
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }
}
