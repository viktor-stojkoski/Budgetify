import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  IFileForUpload,
  TranslationKeys as SharedTranslationKeys,
  SnackbarService,
  enumToTranslationEnum,
  getEnumKeyFromValue
} from '@budgetify/shared';
import { Observable, distinctUntilChanged, map, startWith, take, takeUntil } from 'rxjs';
import { TransactionType } from '../../models/transaction.enum';
import {
  IAccountResponse,
  ICategoryResponse,
  ICurrencyResponse,
  IMerchantResponse
} from '../../models/transaction.model';
import { TransactionService } from '../../services/transaction.service';
import { fileStatics } from '../../static/fileStatics';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-transaction',
  templateUrl: './create-transaction.component.html',
  styleUrls: ['./create-transaction.component.scss']
})
export class CreateTransactionComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public types = enumToTranslationEnum(TransactionType);
  public transactionTypeExpense = getEnumKeyFromValue(TransactionType, TransactionType.EXPENSE);
  public accounts?: IAccountResponse[];
  public categories?: ICategoryResponse[];
  public currencies?: ICurrencyResponse[];
  public merchants?: IMerchantResponse[];
  public filteredAccounts$?: Observable<IAccountResponse[] | undefined>;
  public filteredCategories$?: Observable<ICategoryResponse[] | undefined>;
  public filteredCurrencies$?: Observable<ICurrencyResponse[] | undefined>;
  public filteredMerchants$?: Observable<IMerchantResponse[] | undefined>;
  public isLoading = true;
  public selectedFiles: IFileForUpload[] = [];
  public isCreating = false;

  public transactionForm = this.formBuilder.group({
    accountUid: ['', Validators.required],
    categoryUid: ['', Validators.required],
    currencyCode: ['', Validators.required],
    merchantUid: [null],
    type: [this.transactionTypeExpense, Validators.required],
    amount: [0, Validators.required],
    date: [new Date(), Validators.required],
    description: ['']
  });

  constructor(
    public dialogRef: MatDialogRef<CreateTransactionComponent>,
    private formBuilder: FormBuilder,
    private transactionService: TransactionService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCurrencies();
    this.getAccounts();
    this.getCategories();
    this.getMerchants();
    this.filterCategoriesByType();
    this.filterMerchantsByCategoryType();
  }

  public createTransaction(): void {
    this.isCreating = true;
    if (this.transactionForm.valid) {
      this.transactionService
        .createTransaction({
          accountUid: this.transactionForm.controls.accountUid.value,
          categoryUid: this.transactionForm.controls.categoryUid.value,
          currencyCode: this.transactionForm.controls.currencyCode.value,
          merchantUid: this.transactionForm.controls.merchantUid.value || null,
          type: this.transactionForm.controls.type.value,
          amount: this.transactionForm.controls.amount.value,
          date: this.transactionForm.controls.date.value,
          description: this.transactionForm.controls.description.value,
          attachments: this.selectedFiles
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
            this.snackbarService.success(this.translationKeys.createTransactionSuccessful);
          },
          error: (error: HttpErrorResponse) => {
            this.snackbarService.showError(error);
            this.isCreating = false;
          }
        });
    } else {
      this.transactionForm.markAllAsTouched();
      this.isCreating = false;
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }

  public displayCurrency(code: string): string {
    return this.currencies?.find((x) => x.code === code)?.name || '';
  }

  public displayAccount(uid: string): string {
    return this.accounts?.find((x) => x.uid === uid)?.name || '';
  }

  public displayCategory(uid: string): string {
    return this.categories?.find((x) => x.uid === uid)?.name || '';
  }

  public displayMerchant(uid: string): string {
    return this.merchants?.find((x) => x.uid === uid)?.name || '';
  }

  public selectFiles($event: Event): void {
    const files = ($event.target as HTMLInputElement).files;

    if (files?.length) {
      for (let i = 0; i < files.length; i++) {
        const file: File = files[i];
        if (file.size > fileStatics.maxBytesForUpload) {
          this.snackbarService.warning(this.translationKeys.uploadFileInvalidSize);
        } else {
          const fileReader: FileReader = new FileReader();
          fileReader.readAsArrayBuffer(file);

          fileReader.onloadend = (readerEvent: ProgressEvent<FileReader>) => {
            if (readerEvent.target && readerEvent.target.readyState == FileReader.DONE) {
              const arrayBuffer: ArrayBuffer = readerEvent.target.result as ArrayBuffer;
              const uintArray: Uint8Array = new Uint8Array(arrayBuffer);

              this.selectedFiles.push({
                content: Array.from(uintArray),
                type: file.type,
                name: file.name,
                size: file.size
              });
            }
          };
        }
      }
    }
  }

  public removeFile(fileName: string): void {
    this.selectedFiles = this.selectedFiles.filter((x) => x.name !== fileName);
  }

  private filterCategoriesByType(): void {
    this.transactionForm.controls.type.valueChanges.subscribe({
      next: () => {
        if (
          this.categories?.find((option) => option.uid === this.transactionForm.controls.categoryUid.value)?.type !==
          this.transactionForm.controls.type.value
        ) {
          this.transactionForm.controls.categoryUid.reset();
        }
        this.transactionForm.controls.accountUid.reset();
        this.transactionForm.controls.categoryUid.reset();
        this.transactionForm.controls.merchantUid.reset();
        this.filterCategories();
      }
    });
  }

  private filterMerchantsByCategoryType(): void {
    this.transactionForm.controls.categoryUid.valueChanges.subscribe({
      next: () => {
        if (
          this.categories?.find((option) => option.uid === this.transactionForm.controls.categoryUid.value)?.name !==
          this.merchants?.find((option) => option.uid === this.transactionForm.controls.merchantUid.value)?.categoryName
        ) {
          this.transactionForm.controls.merchantUid.reset();
        }
        this.filterMerchants();
      }
    });
  }

  private getMerchants(): void {
    this.transactionService
      .getMerchants()
      .pipe(take(1))
      .subscribe({
        next: (result) => {
          this.merchants = result.value;
          this.isLoading = false;
          this.filterMerchants();
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }

  private filterMerchants(): void {
    this.filteredMerchants$ = this.transactionForm.controls.merchantUid.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filterMerchant(value || ''))
    );
  }

  private filterMerchant(value: string): IMerchantResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.merchants?.filter(
      (option) =>
        !this.transactionForm.controls.categoryUid.value ||
        (option.name.toLowerCase().includes(filterValue) &&
          option.categoryName ===
            this.categories?.find((category) => category.uid === this.transactionForm.controls.categoryUid.value)?.name)
    );
  }

  private getCategories(): void {
    this.transactionService
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
    this.filteredCategories$ = this.transactionForm.controls.categoryUid.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filterCategory(value || ''))
    );
  }

  private filterCategory(value: string): ICategoryResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.categories?.filter(
      (option) =>
        !this.transactionForm.controls.type.value ||
        (option.name.toLowerCase().includes(filterValue) && option.type === this.transactionForm.controls.type.value)
    );
  }

  private getAccounts(): void {
    this.transactionService
      .getAccounts()
      .pipe(take(1))
      .subscribe({
        next: (result) => {
          this.accounts = result.value;
          this.isLoading = false;
          this.filterAccounts();
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }

  private filterAccounts(): void {
    this.filteredAccounts$ = this.transactionForm.controls.accountUid.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filterAccount(value || ''))
    );
  }

  private filterAccount(value: string): IAccountResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.accounts?.filter((option) => option.name.toLowerCase().includes(filterValue));
  }

  private getCurrencies(): void {
    this.transactionService
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
    this.filteredCurrencies$ = this.transactionForm.controls.currencyCode.valueChanges.pipe(
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
