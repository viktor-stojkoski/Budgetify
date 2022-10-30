import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DestroyBaseComponent, DialogService, SnackbarService } from '@budgetify/shared';
import { take, takeUntil } from 'rxjs';
import { CategoryType } from '../../models/category.enum';
import { ICategoryResponse } from '../../models/category.model';
import { CategoryService } from '../../services/category.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateCategoryComponent } from '../create-category/create-category.component';

@Component({
  selector: 'app-categories-table',
  templateUrl: './categories-table.component.html',
  styleUrls: ['./categories-table.component.scss']
})
export class CategoriesTableComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public dataSource!: MatTableDataSource<ICategoryResponse>;
  public displayedColumns = ['name', 'type'];
  public isLoading = true;
  public type = CategoryType;
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
    private categoryService: CategoryService,
    private snackbarService: SnackbarService,
    private dialogService: DialogService,
    private router: Router
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCategories();
  }

  public openCreateCategoryDialog(): void {
    this.dialogService
      .open(CreateCategoryComponent)
      .afterClosed()
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: () => this.getCategories()
      });
  }

  public openCategoryDetails(uid: string): void {
    this.router.navigateByUrl(`categories/${uid}`);
  }

  public applyFilter(event: Event): void {
    this.filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  private getCategories(): void {
    this.categoryService
      .getCategories()
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
