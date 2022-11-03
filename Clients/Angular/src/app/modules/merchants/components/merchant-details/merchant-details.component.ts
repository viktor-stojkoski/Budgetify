import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { concatMap, takeUntil, tap } from 'rxjs';
import { IMerchantResponse } from '../../models/merchant.model';
import { MerchantService } from '../../services/merchant.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-merchant-details',
  templateUrl: './merchant-details.component.html',
  styleUrls: ['./merchant-details.component.scss']
})
export class MerchantDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public merchantUid: string | null = '';
  public merchant?: IMerchantResponse;
  public isLoading = true;

  constructor(
    private merchantService: MerchantService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getMerchant();
  }

  private getMerchant(): void {
    this.isLoading = true;
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.merchantUid = params.get('merchantUid'))),
        concatMap(() => this.merchantService.getMerchant(this.merchantUid))
      )
      .subscribe({
        next: (result) => {
          this.merchant = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
