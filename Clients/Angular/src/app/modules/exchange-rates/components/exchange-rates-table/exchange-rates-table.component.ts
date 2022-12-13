import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import {
  DestroyBaseComponent,
  DialogActionButton,
  DialogService,
  IDialogResponseData,
  SnackbarService
} from '@budgetify/shared';
import { take, takeUntil } from 'rxjs';
import { IExchangeRateResponse } from '../../models/exchange-rate.model';
import { ExchangeRateService } from '../../services/exchange-rate.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateExchangeRateComponent } from '../create-exchange-rate/create-exchange-rate.component';

@Component({
  selector: 'app-exchange-rates-table',
  templateUrl: './exchange-rates-table.component.html',
  styleUrls: ['./exchange-rates-table.component.scss']
})
export class ExchangeRatesTableComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public dataSource!: MatTableDataSource<IExchangeRateResponse>;
  public displayedColumns = ['fromCurrencyCode', 'toCurrencyCode', 'fromDate', 'toDate', 'rate'];
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
    private exchangeRateService: ExchangeRateService,
    private snackbarService: SnackbarService,
    private dialogService: DialogService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getExchangeRates();
  }

  public openCreateExchangeRateDialog(): void {
    this.dialogService
      .open(CreateExchangeRateComponent)
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response.action === DialogActionButton.Ok) {
            this.getExchangeRates();
          }
        }
      });
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  private getExchangeRates(): void {
    this.exchangeRateService
      .getExchangeRates()
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
