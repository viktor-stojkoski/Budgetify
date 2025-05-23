import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService, TranslationKeys as SharedTranslationKeys } from '@budgetify/shared';
import { concatMap, take, takeUntil, tap } from 'rxjs';
import { IExchangeRateResponse } from '../../models/exchange-rate.model';
import { ExchangeRateService } from '../../services/exchange-rate.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-exchange-rate-details',
  templateUrl: './exchange-rate-details.component.html',
  styleUrls: ['./exchange-rate-details.component.scss']
})
export class ExchangeRateDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public exchangeRateUid: string | null = '';
  public exchangeRate?: IExchangeRateResponse;
  public isLoading = false;
  public isEditing = false;

  public exchangeRateForm = this.formBuilder.group({
    fromCurrencyCode: ['', Validators.required],
    toCurrencyCode: ['', Validators.required],
    fromDate: [new Date()],
    toDate: [new Date()],
    rate: [0, Validators.required]
  });

  constructor(
    private exchangeRateService: ExchangeRateService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getExchangeRate();
    this.exchangeRateForm.controls.fromCurrencyCode.disable();
    this.exchangeRateForm.controls.toCurrencyCode.disable();
    this.exchangeRateForm.controls.toDate.disable();
  }

  public toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (this.exchangeRate) {
      this.exchangeRateForm.patchValue(this.exchangeRate);
    }
  }

  public editExchangeRate(): void {
    this.isLoading = true;
    if (this.exchangeRateForm.valid) {
      this.exchangeRateService
        .updateExchangeRate(this.exchangeRateUid, {
          fromDate: this.exchangeRateForm.controls.fromDate.value,
          rate: this.exchangeRateForm.controls.rate.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.exchangeRate = {
              toCurrencyCode: this.exchangeRate?.toCurrencyCode as string,
              fromCurrencyCode: this.exchangeRate?.fromCurrencyCode as string,
              fromDate: this.exchangeRateForm.controls.fromDate.value as Date,
              rate: this.exchangeRateForm.controls.rate.value as number
            };
            this.snackbarService.success(this.translationKeys.updateExchangeRateSuccessful);
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

  private getExchangeRate(): void {
    this.isLoading = true;
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.exchangeRateUid = params.get('exchangeRateUid'))),
        concatMap(() => this.exchangeRateService.getExchangeRate(this.exchangeRateUid))
      )
      .subscribe({
        next: (result) => {
          this.exchangeRate = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
