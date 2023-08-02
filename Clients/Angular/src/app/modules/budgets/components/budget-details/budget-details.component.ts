import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, TranslationKeys as SharedTranslationKeys, SnackbarService } from '@budgetify/shared';
import { concatMap, take, takeUntil, tap } from 'rxjs';
import { IBudgetResponse } from '../../models/budget.model';
import { BudgetService } from '../../services/budget.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-budget-details',
  templateUrl: './budget-details.component.html',
  styleUrls: ['./budget-details.component.scss']
})
export class BudgetDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public budgetUid: string | null = '';
  public budget?: IBudgetResponse;
  public isLoading = false;
  public isEditing = false;

  public budgetForm = this.formBuilder.group({
    name: ['', Validators.required],
    amount: [0, Validators.required]
  });

  constructor(
    private budgetService: BudgetService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getBudget();
  }

  public toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (this.budget) {
      this.budgetForm.patchValue(this.budget);
    }
  }

  public editBudget(): void {
    this.isLoading = true;
    if (this.budgetForm.valid) {
      this.budgetService
        .updateBudget(this.budgetUid, {
          name: this.budgetForm.controls.name.value,
          amount: this.budgetForm.controls.amount.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.budget = {
              ...(this.budgetForm.value as IBudgetResponse),
              categoryName: this.budget?.categoryName as string,
              startDate: this.budget?.startDate as Date,
              endDate: this.budget?.endDate as Date,
              amountSpent: this.budget?.amountSpent as number
            };
            this.snackbarService.success(this.translationKeys.updateBudgetSuccessful);
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

  private getBudget(): void {
    this.isLoading = true;
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.budgetUid = params.get('budgetUid'))),
        concatMap(() => this.budgetService.getBudget(this.budgetUid))
      )
      .subscribe({
        next: (result) => {
          this.budget = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
