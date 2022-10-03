import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { TranslationKeys } from '../static/translationKeys';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  private translationKeys = TranslationKeys;
  private snackBarConfig: MatSnackBarConfig<any> = {
    duration: 8000,
    direction: 'ltr',
    horizontalPosition: 'center',
    verticalPosition: 'top'
  };

  constructor(private snackBar: MatSnackBar, private translateService: TranslateService) {}

  public success(message: string, translate = true): void {
    this.snackBar.open(
      translate ? this.translateService.instant(message) : message,
      this.translateService.instant(this.translationKeys.mainActionButtonOk),
      {
        ...this.snackBarConfig,
        panelClass: 'snackbar-success'
      }
    );
  }

  public warning(message: string): void {
    this.snackBar.open(message, this.translateService.instant(this.translationKeys.mainActionButtonOk), {
      ...this.snackBarConfig,
      panelClass: 'snackbar-warning'
    });
  }

  public error(message: string): void {
    this.snackBar.open(message, this.translateService.instant(this.translationKeys.mainActionButtonOk), {
      ...this.snackBarConfig,
      panelClass: 'snackbar-error'
    });
  }

  public showError(error: HttpErrorResponse): void {
    const isServerError = error.status.toString().startsWith('5');

    isServerError
      ? this.error(this.translateService.instant(this.translationKeys.internalServerError))
      : this.warning(this.translateService.instant(`errorCodes.${error.error}`));
  }
}
