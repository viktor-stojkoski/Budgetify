import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DestroyBaseComponent, SnackbarService, TranslationKeys as SharedTranslationKeys } from '@budgetify/shared';
import { concatMap, distinctUntilChanged, map, Observable, startWith, take, takeUntil, tap } from 'rxjs';
import { ICategoryResponse, IMerchantResponse } from '../../models/merchant.model';
import { MerchantService } from '../../services/merchant.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-merchant-details',
  templateUrl: './merchant-details.component.html',
  styleUrls: ['./merchant-details.component.scss']
})
export class MerchantDetailsComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public merchantUid: string | null = '';
  public merchant?: IMerchantResponse;
  public categories?: ICategoryResponse[];
  public filteredCategories$?: Observable<ICategoryResponse[] | undefined>;
  public isLoading = true;
  public isEditing = false;

  public merchantForm = this.formBuilder.group({
    name: ['', Validators.required],
    categoryUid: ['', Validators.required]
  });

  constructor(
    private merchantService: MerchantService,
    private activatedRoute: ActivatedRoute,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getMerchant();
    this.getCategories();
  }

  public toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (this.merchant) {
      this.merchantForm.patchValue(this.merchant);
    }
  }

  public editMerchant(): void {
    this.isLoading = true;
    if (this.merchantForm.valid) {
      this.merchantService
        .updateMerchant(this.merchantUid, {
          name: this.merchantForm.controls.name.value,
          categoryUid: this.merchantForm.controls.categoryUid.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.merchant = {
              uid: this.merchantUid as string,
              name: this.merchantForm.controls.name.value as string,
              categoryName: this.displayCategory(this.merchantForm.controls.categoryUid.value as string)
            };
            this.snackbarService.success(this.translationKeys.updateMerchantSuccessful);
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

  public displayCategory(uid: string): string {
    return this.categories?.find((x) => x.uid === uid)?.name || '';
  }

  private getCategories(): void {
    this.merchantService
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
    this.filteredCategories$ = this.merchantForm.controls.categoryUid.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.destroyed$),
      map((value) => this.filter(value || ''))
    );
  }

  private filter(value: string): ICategoryResponse[] | undefined {
    const filterValue = value.toLowerCase();

    return this.categories?.filter((option) => option.name.toLowerCase().includes(filterValue));
  }

  private getMerchant(): void {
    this.isLoading = true;
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.destroyed$),
        tap((params: ParamMap) => (this.merchantUid = params.get('merchantUid'))),
        concatMap(() => this.merchantService.getMerchant(this.merchantUid))
      )
      .subscribe({
        next: (result) => {
          this.merchant = result.value;
          if (this.merchant) {
            this.merchantForm.patchValue(this.merchant);
          }
          this.isLoading = false;
        },
        error: (error) => {
          this.snackbarService.showError(error);
          this.isLoading = false;
        }
      });
  }
}
