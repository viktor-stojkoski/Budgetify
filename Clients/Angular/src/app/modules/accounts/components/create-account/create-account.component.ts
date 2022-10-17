import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  enumToTranslationEnum,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { distinctUntilChanged, map, Observable, take, takeUntil } from 'rxjs';
import { AccountType } from '../../models/account.enum';
import { ICurrencyResponse } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public types = enumToTranslationEnum(AccountType);
  public currencies?: ICurrencyResponse[];
  public filteredCurrencies$?: Observable<ICurrencyResponse[] | undefined>;
  public isLoading = true;

  public accountForm = this.formBuilder.group({
    name: ['', Validators.required],
    type: ['', Validators.required],
    balance: [0, Validators.required],
    currencyCode: ['', Validators.required],
    description: ['']
  });

  constructor(
    public dialogRef: MatDialogRef<CreateAccountComponent>,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCurrencies();
    this.filterCurrencies();
  }

  public createAccount(): void {
    if (this.accountForm.valid) {
      this.accountService
        .createAccount({
          name: this.accountForm.controls.name.value,
          type: this.accountForm.controls.type.value,
          balance: this.accountForm.controls.balance.value,
          currencyCode: this.accountForm.controls.currencyCode.value,
          description: this.accountForm.controls.description.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close();
            this.snackbarService.success(this.translationKeys.createAccountSuccessful);
          },
          error: (error) => this.snackbarService.showError(error)
        });
    } else {
      this.accountForm.markAllAsTouched();
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close();
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
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }

  private filterCurrencies() {
    this.filteredCurrencies$ = this.accountForm.controls.currencyCode.valueChanges.pipe(
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
}
