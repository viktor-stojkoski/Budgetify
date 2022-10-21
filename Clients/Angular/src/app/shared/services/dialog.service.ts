import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';

@Injectable()
export class DialogService {
  constructor(private dialog: MatDialog) {}

  public open(
    component: ComponentType<unknown>,
    config: MatDialogConfig<any> = {
      width: '600px'
    }
  ): MatDialogRef<unknown, any> {
    return this.dialog.open(component, config);
  }
}
