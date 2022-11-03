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
import { IMerchantResponse } from '../../models/merchant.model';
import { MerchantService } from '../../services/merchant.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateMerchantComponent } from '../create-merchant/create-merchant.component';

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

  constructor(
    private merchantService: MerchantService,
    private snackbarService: SnackbarService,
    private dialogService: DialogService,
    private router: Router
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getMerchants();
  }

  public openCreateMerchantDialog(): void {
    this.dialogService
      .open(CreateMerchantComponent)
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (response: IDialogResponseData) => {
          if (response.action === DialogActionButton.Ok) {
            this.getMerchants();
          }
        }
      });
  }

  public openMerchantDetails(uid: string): void {
    this.router.navigateByUrl(`merchants/${uid}`);
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  private getMerchants(): void {
    this.merchantService
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
