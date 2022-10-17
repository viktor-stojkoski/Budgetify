import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import {
  DestroyBaseComponent,
  enumToTranslationEnum,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { concatMap, distinctUntilChanged, map, Observable, take, takeUntil, tap } from 'rxjs';
import { AccountType } from '../../models/account.enum';
import { IAccountResponse, ICurrencyResponse } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent extends DestroyBaseComponent implements OnInit {
  public accountUid: string | null = '';
  public account?: IAccountResponse;
  public isLoading = true;
  public isEditing = false;
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public type = AccountType;
  public types = enumToTranslationEnum(AccountType);
  public currencies?: ICurrencyResponse[];
  public filteredCurrencies?: Observable<ICurrencyResponse[] | undefined>;

  public accountForm = this.formBuilder.group({
    name: ['', Validators.required],
    type: ['', Validators.required],
    balance: [0, Validators.required],
    currencyCode: ['', Validators.required],
    description: ['']
  });

  constructor(
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getAccount();
    this.getCurrencies();
    this.filterCurrencies();
  }

  public toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (this.account) {
      this.accountForm.patchValue(this.account);
    }
  }

  public editAccount(): void {
    if (this.accountForm.valid) {
      this.accountService
        .updateAccount(this.accountUid, {
          name: this.accountForm.controls.name.value,
          type: this.accountForm.controls.type.value,
          balance: this.accountForm.controls.balance.value,
          currencyCode: this.accountForm.controls.currencyCode.value,
          description: this.accountForm.controls.description.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.account = this.accountForm.value as IAccountResponse;
            this.snackbarService.success(this.translationKeys.editAccountSuccessful);
            this.isEditing = false;
          },
          error: (error: HttpErrorResponse) => this.snackbarService.showError(error)
        });
    }
  }

  public displayCurrency(code: string): string {
    return this.currencies?.find((x) => x.code === code)?.name || '';
  }

  private getCurrencies(): void {
    this.accountService
      .getCurrencies()
      .pipe(take(1))
      .subscribe({
        next: (result) => {
          this.currencies = result.value;
          this.isLoading = false;
        },
        error: (error) => {
          console.error(error);
          this.isLoading = false;
        }
      });
  }

  private filterCurrencies() {
    this.filteredCurrencies = this.accountForm.controls.currencyCode.valueChanges.pipe(
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filter(value || ''))
    );
  }

  private filter(value: string): ICurrencyResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.currencies?.filter(
      (option) => option.name.toLowerCase().includes(filterValue) || option.code.toLowerCase().includes(filterValue)
    );
  }

  private getAccount(): void {
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.accountUid = params.get('accountUid'))),
        concatMap(() => this.accountService.getAccount(this.accountUid))
      )
      .subscribe({
        next: (result) => {
          this.account = result.value;
          if (this.account) {
            this.accountForm.patchValue(this.account);
          }
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
