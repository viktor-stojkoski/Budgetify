import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { concatMap, takeUntil, tap } from 'rxjs';
import { TransactionType } from '../../models/transaction.enum';
import { ITransactionResponse } from '../../models/transaction.model';
import { TransactionService } from '../../services/transaction.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-transaction-details',
  templateUrl: './transaction-details.component.html',
  styleUrls: ['./transaction-details.component.scss']
})
export class TransactionDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public transactionUid: string | null = '';
  public transaction?: ITransactionResponse;
  public isLoading = false;
  public type = TransactionType;

  constructor(
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getTransaction();
  }

  private getTransaction(): void {
    this.isLoading = true;
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.transactionUid = params.get('transactionUid'))),
        concatMap(() => this.transactionService.getTransaction(this.transactionUid))
      )
      .subscribe({
        next: (result) => {
          this.transaction = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
