import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DestroyBaseComponent, DialogService } from '@budgetify/shared';
import { AccountType } from '../../models/account.enum';
import { IAccountResponse } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateAccountComponent } from '../create-account/create-account.component';

@Component({
  selector: 'app-accounts-table',
  templateUrl: './accounts-table.component.html',
  styleUrls: ['./accounts-table.component.scss']
})
export class AccountsTableComponent extends DestroyBaseComponent implements OnInit {
  public dataSource!: MatTableDataSource<IAccountResponse>;
  public displayedColumns = ['name', 'type', 'balance', 'currencyCode', 'description'];
  public readonly translationKeys = TranslationKeys;
  public isLoading = true;
  public type = AccountType;

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

  constructor(private accountService: AccountService, private dialogService: DialogService, private router: Router) {
    super();
  }

  public ngOnInit(): void {
    this.getAccounts();
  }

  public openCreateAccountDialog() {
    this.dialogService
      .open(CreateAccountComponent)
      .afterClosed()
      .subscribe({
        next: () => this.getAccounts()
      });
  }

  public openAccountDetails(uid: any) {
    console.log(uid);
    this.router.navigateByUrl(`accounts/${uid}`);
  }

  public applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  private getAccounts(): void {
    this.accountService.getAccounts().subscribe({
      next: (response) => {
        this.dataSource = new MatTableDataSource(response.value);
        this.isLoading = false;
      },
      error: (error) => {
        console.error(error);
        this.isLoading = false;
      }
    });
  }
}
