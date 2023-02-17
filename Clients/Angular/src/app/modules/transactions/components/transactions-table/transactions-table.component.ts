import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import {
  DestroyBaseComponent,
  DialogActionButton,
  DialogService,
  IDialogResponseData,
  SnackbarService
} from '@budgetify/shared';
import { take, takeUntil } from 'rxjs';
import { TransactionType } from '../../models/transaction.enum';
import { IDeleteTransactionDialogData, ITransactionResponse } from '../../models/transaction.model';
import { TransactionService } from '../../services/transaction.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateTransactionComponent } from '../create-transaction/create-transaction.component';
import { DeleteTransactionComponent } from '../delete-transaction/delete-transaction.component';

@Component({
  selector: 'app-transactions-table',
  templateUrl: './transactions-table.component.html',
  styleUrls: ['./transactions-table.component.scss']
})
export class TransactionsTableComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public dataSource!: MatTableDataSource<ITransactionResponse>;
  public displayedColumns = [
    'accountName',
    'categoryName',
    'merchantName',
    'type',
    'amount',
    'currencyCode',
    'date',
    'description',
    'isVerified',
    'actions'
  ];
  public isLoading = true;
  public type = TransactionType;
  public filterValue = '';

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
    private snackbarService: SnackbarService,
    private dialogService: DialogService,
    private router: Router
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getTransactions();
  }

  public openCreateTransactionDialog(): void {
    this.dialogService
      .open(CreateTransactionComponent)
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.getTransactions();
          }
        }
      });
  }

  public openDeleteTransactionDialog(uid: string): void {
    this.dialogService
      .open(DeleteTransactionComponent, {
        data: {
          uid: uid
        } as IDeleteTransactionDialogData
      })
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.getTransactions();
          }
        }
      });
  }

  public openTransactionDetails(uid: string): void {
    this.router.navigateByUrl(`transactions/${uid}`);
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  private getTransactions(): void {
    this.transactionService
      .getTransactions()
      .pipe(take(1))
      .subscribe({
        next: (response) => {
          this.dataSource = new MatTableDataSource(response.value);
          this.dataSource.sort = this.sort!;
          this.dataSource.paginator = this.paginator!;
          this.filterValue = '';
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
