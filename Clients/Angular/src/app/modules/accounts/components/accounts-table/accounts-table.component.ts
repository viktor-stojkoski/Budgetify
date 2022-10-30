import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DestroyBaseComponent, DialogService, SnackbarService } from '@budgetify/shared';
import { take, takeUntil } from 'rxjs';
import { AccountType } from '../../models/account.enum';
import { IAccountResponse, IDeleteAccountDialogData } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateAccountComponent } from '../create-account/create-account.component';
import { DeleteAccountComponent } from '../delete-account/delete-account.component';

@Component({
  selector: 'app-accounts-table',
  templateUrl: './accounts-table.component.html',
  styleUrls: ['./accounts-table.component.scss']
})
export class AccountsTableComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public dataSource!: MatTableDataSource<IAccountResponse>;
  public displayedColumns = ['name', 'type', 'balance', 'currencyCode', 'description', 'actions'];
  public isLoading = true;
  public type = AccountType;
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
    private accountService: AccountService,
    private dialogService: DialogService,
    private router: Router,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getAccounts();
  }

  public openCreateAccountDialog(): void {
    this.dialogService
      .open(CreateAccountComponent)
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: () => this.getAccounts()
      });
  }

  public openDeleteAccountDialog(name: string, uid: string): void {
    this.dialogService
      .open(DeleteAccountComponent, {
        data: {
          name: name,
          uid: uid
        } as IDeleteAccountDialogData
      })
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: () => this.getAccounts()
      });
  }

  public openAccountDetails(uid: string): void {
    this.router.navigateByUrl(`accounts/${uid}`);
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  private getAccounts(): void {
    this.accountService
      .getAccounts()
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
