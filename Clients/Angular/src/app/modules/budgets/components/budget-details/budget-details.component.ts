import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { concatMap, takeUntil, tap } from 'rxjs';
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
  public budgetUid: string | null = '';
  public budget?: IBudgetResponse;
  public isLoading = false;

  constructor(
    private budgetService: BudgetService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getBudget();
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
