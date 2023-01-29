import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import {
  DestroyBaseComponent,
  DialogActionButton,
  DialogService,
  enumToTranslationEnum,
  IDialogResponseData,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { concatMap, distinctUntilChanged, map, Observable, startWith, take, takeUntil, tap } from 'rxjs';
import { TransactionType } from '../../models/transaction.enum';
import {
  IAccountResponse,
  ICategoryResponse,
  ICurrencyResponse,
  IDeleteTransactionAttachmentDialogData,
  IDeleteTransactionDialogData,
  IMerchantResponse,
  ITransactionAttachmentResponse,
  ITransactionDetailsResponse,
  ITransactionResponse
} from '../../models/transaction.model';
import { TransactionService } from '../../services/transaction.service';
import { TranslationKeys } from '../../static/translationKeys';
import { AddTransactionAttachmentsComponent } from '../add-transaction-attachments/add-transaction-attachments.component';
import { DeleteTransactionAttachmentComponent } from '../delete-transaction-attachment/delete-transaction-attachment.component';
import { DeleteTransactionComponent } from '../delete-transaction/delete-transaction.component';

@Component({
  selector: 'app-transaction-details',
  templateUrl: './transaction-details.component.html',
  styleUrls: ['./transaction-details.component.scss']
})
export class TransactionDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public transactionUid: string | null = '';
  public transaction?: ITransactionDetailsResponse;
  public isLoading = false;
  public isEditing = false;
  public type = TransactionType;
  public types = enumToTranslationEnum(TransactionType);
  public accounts?: IAccountResponse[];
  public categories?: ICategoryResponse[];
  public currencies?: ICurrencyResponse[];
  public merchants?: IMerchantResponse[];
  public filteredAccounts$?: Observable<IAccountResponse[] | undefined>;
  public filteredCategories$?: Observable<ICategoryResponse[] | undefined>;
  public filteredCurrencies$?: Observable<ICurrencyResponse[] | undefined>;
  public filteredMerchants$?: Observable<IMerchantResponse[] | undefined>;
  public dataSource!: MatTableDataSource<ITransactionAttachmentResponse>;
  public displayedColumns = ['name', 'createdOn', 'actions'];

  public transactionForm = this.formBuilder.group({
    accountUid: ['', Validators.required],
    categoryUid: ['', Validators.required],
    currencyCode: ['', Validators.required],
    merchantUid: [''],
    type: ['', Validators.required],
    amount: [0, Validators.required],
    date: [new Date(), Validators.required],
    description: ['']
  });

  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort?: MatSort;

  @ViewChild(MatPaginator) set matPaginator(paginator: MatPaginator) {
    if (paginator && !this.dataSource.paginator) {
      this.dataSource.paginator = paginator;
    }
  }
  @ViewChild(MatSort) set matSort(sort: MatSort) {
    if (sort && !this.dataSource.sort) {
      this.dataSource.sort = sort;
    }
  }

  constructor(
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder,
    private dialogService: DialogService,
    private router: Router
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getTransaction();
    this.getAccounts();
    this.getCategories();
    this.getCurrencies();
    this.getMerchants();
  }

  public toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (this.transaction) {
      this.transactionForm.patchValue(this.transaction);
    }
  }

  public editTransaction(): void {
    this.isLoading = true;
    if (this.transactionForm.valid) {
      this.transactionService
        .updateTransaction(this.transactionUid, {
          accountUid: this.transactionForm.controls.accountUid.value,
          categoryUid: this.transactionForm.controls.categoryUid.value,
          currencyCode: this.transactionForm.controls.currencyCode.value,
          merchantUid: this.transactionForm.controls.merchantUid.value || null,
          type: this.transactionForm.controls.type.value,
          amount: this.transactionForm.controls.amount.value,
          date: this.transactionForm.controls.date.value,
          description: this.transactionForm.controls.description.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.transaction = {
              ...(this.transaction?.transactionAttachments ?? []),
              ...(this.transactionForm.value as ITransactionResponse),
              accountName: this.displayAccount(this.transactionForm.controls.accountUid.value as string),
              categoryName: this.displayCategory(this.transactionForm.controls.categoryUid.value as string),
              merchantName: this.displayMerchant(this.transactionForm.controls.merchantUid.value as string)
            };
            this.snackbarService.success(this.translationKeys.updateTransactionSuccessful);
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

  public openDeleteTransactionDialog(): void {
    this.dialogService
      .open(DeleteTransactionComponent, {
        data: {
          uid: this.transactionUid
        } as IDeleteTransactionDialogData
      })
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.router.navigateByUrl('transactions');
          }
        }
      });
  }

  public downloadAttachment(url: string): void {
    window.open(url, '_blank');
  }

  public openDeleteTransactionAttachmentDialog(attachmentUid: string, name: string): void {
    this.dialogService
      .open(DeleteTransactionAttachmentComponent, {
        data: {
          transactionUid: this.transactionUid,
          attachmentUid: attachmentUid,
          name: name
        } as IDeleteTransactionAttachmentDialogData
      })
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.getTransaction();
          }
        }
      });
  }

  public openAddTransactionAttachmentsDialog(): void {
    this.dialogService
      .open(AddTransactionAttachmentsComponent, {
        data: this.transactionUid
      })
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.getTransaction();
          }
        }
      });
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
          if (this.transaction) {
            this.transactionForm.patchValue(this.transaction);
            this.dataSource = new MatTableDataSource(this.transaction.transactionAttachments);
            this.dataSource.sort = this.sort!;
            this.dataSource.paginator = this.paginator!;
          }
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
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

    return this.merchants?.filter((option) => option.name.toLowerCase().includes(filterValue));
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

    return this.categories?.filter((option) => option.name.toLowerCase().includes(filterValue));
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
