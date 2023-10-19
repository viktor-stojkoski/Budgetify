import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  TranslationKeys as SharedTranslationKeys,
  SnackbarService
} from '@budgetify/shared';
import { Observable, distinctUntilChanged, map, startWith, take, takeUntil } from 'rxjs';
import { ICategoryResponse, ICurrencyResponse } from '../../models/budget.model';
import { BudgetService } from '../../services/budget.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-budget',
  templateUrl: './create-budget.component.html',
  styleUrls: ['./create-budget.component.scss']
})
export class CreateBudgetComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public categories?: ICategoryResponse[];
  public currencies?: ICurrencyResponse[];
  public filteredCategories$?: Observable<ICategoryResponse[] | undefined>;
  public filteredCurrencies$?: Observable<ICurrencyResponse[] | undefined>;
  public isLoading = true;

  public budgetForm = this.formBuilder.group({
    name: ['', Validators.required],
    categoryUid: ['', Validators.required],
    currencyCode: ['', Validators.required],
    startDate: [new Date(), Validators.required],
    endDate: [new Date(), Validators.required],
    amount: [0, Validators.required],
    amountSpent: [0, Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<CreateBudgetComponent>,
    private formBuilder: FormBuilder,
    private budgetService: BudgetService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCategories();
    this.getCurrencies();
  }

  public createBudget(): void {
    if (this.budgetForm.valid) {
      this.budgetService
        .createBudget({
          name: this.budgetForm.controls.name.value,
          categoryUid: this.budgetForm.controls.categoryUid.value,
          currencyCode: this.budgetForm.controls.currencyCode.value,
          startDate: this.budgetForm.controls.startDate.value,
          endDate: this.budgetForm.controls.endDate.value,
          amount: this.budgetForm.controls.amount.value,
          amountSpent: this.budgetForm.controls.amountSpent.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
            this.snackbarService.success(this.translationKeys.createBudgetSuccessful);
          },
          error: (error) => this.snackbarService.showError(error)
        });
    } else {
      this.budgetForm.markAllAsTouched();
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }

  public displayCategory(uid: string): string {
    return this.categories?.find((x) => x.uid === uid)?.name || '';
  }

  public displayCurrency(code: string): string {
    return this.currencies?.find((x) => x.code === code)?.name || '';
  }

  private getCategories(): void {
    this.budgetService
      .getCategories()
      .pipe(take(1))
      .subscribe({
        next: (result) => {
          this.categories = result.value;
          this.isLoading = false;
          this.filterCategories();
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }

  private filterCategories(): void {
    this.filteredCategories$ = this.budgetForm.controls.categoryUid.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filter(value || ''))
    );
  }

  private filter(value: string): ICategoryResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.categories?.filter((option) => option.name.toLowerCase().includes(filterValue));
  }

  private getCurrencies(): void {
    this.budgetService
      .getCurrencies()
      .pipe(take(1))
      .subscribe({
        next: (result) => {
          this.currencies = result.value;
          this.isLoading = false;
          this.filterCurrencies();
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }

  private filterCurrencies(): void {
    this.filteredCurrencies$ = this.budgetForm.controls.currencyCode.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filterCurrency(value || ''))
    );
  }

  private filterCurrency(value: string): ICurrencyResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.currencies?.filter(
      (option) => option.name.toLowerCase().includes(filterValue) || option.code.toLowerCase().includes(filterValue)
    );
  }
}
