import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';

@Injectable()
export class DialogService {
  constructor(private dialog: MatDialog) {}

  public open(
    component: ComponentType<unknown>,
    width: string = '600px',
    data: MatDialogConfig<any> | null = null
  ): MatDialogRef<unknown, any> {
    return this.dialog.open(component, {
      width: width,
      data: data
    });
  }
}
