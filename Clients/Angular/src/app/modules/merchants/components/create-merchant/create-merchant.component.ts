import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { distinctUntilChanged, map, Observable, startWith, take, takeUntil } from 'rxjs';
import { ICategoryResponse } from '../../models/merchant.model';
import { MerchantService } from '../../services/merchant.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-merchant',
  templateUrl: './create-merchant.component.html',
  styleUrls: ['./create-merchant.component.scss']
})
export class CreateMerchantComponent extends DestroyBaseComponent implements OnInit {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public categories?: ICategoryResponse[];
  public filteredCategories$?: Observable<ICategoryResponse[] | undefined>;
  public isLoading = true;

  public merchantForm = this.formBuilder.group({
    name: ['', Validators.required],
    categoryUid: ['', Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<CreateMerchantComponent>,
    private formBuilder: FormBuilder,
    private merchantService: MerchantService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.getCategories();
  }

  public createMerchant(): void {
    if (this.merchantForm.valid) {
      this.merchantService
        .createMerchant({
          name: this.merchantForm.controls.name.value,
          categoryUid: this.merchantForm.controls.categoryUid.value
        })
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
            this.snackbarService.success(this.translationKeys.createMerchantSuccessful);
          },
          error: (error) => this.snackbarService.showError(error)
        });
    } else {
      this.merchantForm.markAllAsTouched();
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
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
}
