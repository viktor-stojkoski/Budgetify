import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DestroyBaseComponent, SnackbarService } from '@budgetify/shared';
import { take } from 'rxjs';
import { IMerchantResponse } from '../../models/merchant.model';
import { MerchantService } from '../../services/merchant.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-merchants-table',
  templateUrl: './merchants-table.component.html',
  styleUrls: ['./merchants-table.component.scss']
})
export class MerchantsTableComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public dataSource!: MatTableDataSource<IMerchantResponse>;
  public displayedColumns = ['name', 'category'];
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

  constructor(private merchantsService: MerchantService, private snackbarService: SnackbarService) {
    super();
  }

  public ngOnInit(): void {
    this.getMerchants();
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  private getMerchants(): void {
    this.merchantsService
      .getMerchants()
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
