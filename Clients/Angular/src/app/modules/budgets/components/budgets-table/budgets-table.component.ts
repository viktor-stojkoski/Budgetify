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
import { IBudgetResponse, IDeleteBudgetDialogData } from '../../models/budget.model';
import { BudgetService } from '../../services/budget.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateBudgetComponent } from '../create-budget/create-budget.component';
import { DeleteBudgetComponent } from '../delete-budget/delete-budget.component';

@Component({
  selector: 'app-budgets-table',
  templateUrl: './budgets-table.component.html',
  styleUrls: ['./budgets-table.component.scss']
})
export class BudgetsTableComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public dataSource!: MatTableDataSource<IBudgetResponse>;
  public displayedColumns = [
    'name',
    'categoryName',
    'startDate',
    'endDate',
    'amount',
    'currencyCode',
    'amountSpent',
    'actions'
  ];
  public isLoading = true;
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
    private budgetService: BudgetService,
    private snackbarService: SnackbarService,
    private dialogService: DialogService,
    private router: Router
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getBudgets();
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  public openCreateBudgetDialog(): void {
    this.dialogService
      .open(CreateBudgetComponent)
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.getBudgets();
          }
        }
      });
  }

  public openBudgetDetails(uid: string): void {
    this.router.navigateByUrl(`budgets/${uid}`);
  }

  public openDeleteBudgetDialog(name: string, uid: string): void {
    this.dialogService
      .open(DeleteBudgetComponent, {
        data: {
          name: name,
          uid: uid
        } as IDeleteBudgetDialogData
      })
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response?.action === DialogActionButton.Ok) {
            this.getBudgets();
          }
        }
      });
  }

  private getBudgets(): void {
    this.budgetService
      .getBudgets()
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
