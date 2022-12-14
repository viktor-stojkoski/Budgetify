import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { distinctUntilChanged, map, Observable, startWith, take, takeUntil } from 'rxjs';
import { ICurrencyResponse } from '../../models/exchange-rate.model';
import { ExchangeRateService } from '../../services/exchange-rate.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-exchange-rate',
  templateUrl: './create-exchange-rate.component.html',
  styleUrls: ['./create-exchange-rate.component.scss']
})
export class CreateExchangeRateComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public currencies?: ICurrencyResponse[];
  public filteredFromCurrencies$?: Observable<ICurrencyResponse[] | undefined>;
  public filteredToCurrencies$?: Observable<ICurrencyResponse[] | undefined>;
  public isLoading = true;

  public exchangeRateForm = this.formBuilder.group({
    fromCurrencyCode: ['', Validators.required],
    toCurrencyCode: ['', Validators.required],
    fromDate: [new Date()],
    rate: [0, Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<CreateExchangeRateComponent>,
    private formBuilder: FormBuilder,
    private exchangeRateService: ExchangeRateService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCurrencies();
  }

  public createExchangeRate(): void {
    if (this.exchangeRateForm.valid) {
      this.exchangeRateService
        .createExchangeRate({
          fromCurrencyCode: this.exchangeRateForm.controls.fromCurrencyCode.value,
          toCurrencyCode: this.exchangeRateForm.controls.toCurrencyCode.value,
          fromDate: this.exchangeRateForm.controls.fromDate.value,
          rate: this.exchangeRateForm.controls.rate.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
            this.snackbarService.success(this.translationKeys.createExchangeRateSuccessful);
          },
          error: (error: HttpErrorResponse) => this.snackbarService.showError(error)
        });
    } else {
      this.exchangeRateForm.markAllAsTouched();
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }

  public displayCurrency(code: string): string {
    return this.currencies?.find((x) => x.code === code)?.name || '';
  }

  private getCurrencies(): void {
    this.exchangeRateService
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
    this.filteredFromCurrencies$ = this.exchangeRateForm.controls.fromCurrencyCode.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filterCurrency(value || ''))
    );

    this.filteredToCurrencies$ = this.exchangeRateForm.controls.toCurrencyCode.valueChanges.pipe(
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
