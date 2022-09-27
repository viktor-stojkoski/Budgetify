import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { TranslationKeys as SharedTranslationKeys } from '@budgetify/shared';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;

  public accountForm = this.formBuilder.group({
    name: ['', Validators.required],
    type: ['', Validators.required],
    balance: [0, Validators.required],
    currencyCode: ['', Validators.required],
    description: ['', Validators.required]
  });

  constructor(
    public dialogRef: MatDialogRef<CreateAccountComponent>,
    private formBuilder: FormBuilder,
    private accountService: AccountService
  ) {}

  public createAccount(): void {
    if (this.accountForm.valid) {
      this.accountService
        .createAccount({
          name: this.accountForm.controls.name.value,
          type: this.accountForm.controls.type.value,
          balance: this.accountForm.controls.balance.value,
          currencyCode: this.accountForm.controls.currencyCode.value,
          description: this.accountForm.controls.description.value
        })
        .subscribe({
          next: () => this.dialogRef.close(),
          error: (error) => console.error(error)
        });
    } else {
      this.accountForm.markAllAsTouched();
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close();
  }
}
