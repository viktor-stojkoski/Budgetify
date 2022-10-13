import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { concatMap, takeUntil } from 'rxjs';
import { AccountType } from '../../models/account.enum';
import { IAccountResponse } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent extends DestroyBaseComponent implements OnInit {
  public account?: IAccountResponse;
  public isLoading = true;
  public readonly translationKeys = TranslationKeys;
  public type = AccountType;

  constructor(
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getAccount();
  }

  private getAccount(): void {
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        concatMap((params: ParamMap) => this.accountService.getAccount(params.get('accountUid')))
      )
      .subscribe({
        next: (result) => {
          this.account = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
