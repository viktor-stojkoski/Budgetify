import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { concatMap, takeUntil, tap } from 'rxjs';
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
  public exchangeRateUid: string | null = '';
  public exchangeRate?: IExchangeRateResponse;
  public isLoading = false;

  constructor(
    private exchangeRateService: ExchangeRateService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getExchangeRate();
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
