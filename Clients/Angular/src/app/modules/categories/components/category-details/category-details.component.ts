import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import {
  DestroyBaseComponent,
  enumToTranslationEnum,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { concatMap, take, takeUntil, tap } from 'rxjs';
import { CategoryType } from '../../models/category.enum';
import { ICategoryResponse } from '../../models/category.model';
import { CategoryService } from '../../services/category.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.scss']
})
export class CategoryDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public categoryUid: string | null = '';
  public category?: ICategoryResponse;
  public isLoading = true;
  public isEditing = false;
  public type = CategoryType;
  public types = enumToTranslationEnum(CategoryType);

  public categoryForm = this.formBuilder.group({
    name: ['', Validators.required],
    type: ['', Validators.required]
  });

  constructor(
    private categoryService: CategoryService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCategory();
  }

  public toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (this.category) {
      this.categoryForm.patchValue(this.category);
    }
  }

  public editCategory(): void {
    this.isLoading = true;
    if (this.categoryForm.valid) {
      this.categoryService
        .updateCategory(this.categoryUid, {
          name: this.categoryForm.controls.name.value,
          type: this.categoryForm.controls.type.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.category = this.categoryForm.value as ICategoryResponse;
            this.snackbarService.success(this.translationKeys.updateCategorySuccessful);
            this.isEditing = false;
            this.isLoading = false;
          },
          error: (error) => {
            this.snackbarService.showError(error);
            this.isLoading = false;
          }
        });
    }
  }

  private getCategory(): void {
    this.isLoading = true;
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.categoryUid = params.get('categoryUid'))),
        concatMap(() => this.categoryService.getCategory(this.categoryUid))
      )
      .subscribe({
        next: (result) => {
          this.category = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
