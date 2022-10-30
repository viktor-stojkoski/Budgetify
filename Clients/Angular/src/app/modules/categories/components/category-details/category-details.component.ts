import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { concatMap, takeUntil, tap } from 'rxjs';
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
  public categoryUid: string | null = '';
  public category?: ICategoryResponse;
  public isLoading = true;
  public type = CategoryType;

  constructor(
    private categoryService: CategoryService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCategory();
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
