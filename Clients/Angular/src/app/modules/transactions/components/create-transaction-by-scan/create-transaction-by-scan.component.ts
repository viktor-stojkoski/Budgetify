import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  IFileForUpload,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { NgxDropzoneChangeEvent } from 'ngx-dropzone';
import { take } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { fileStatics } from '../../static/fileStatics';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-create-transaction-by-scan',
  templateUrl: './create-transaction-by-scan.component.html',
  styleUrls: ['./create-transaction-by-scan.component.scss']
})
export class CreateTransactionByScanComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public isAdding = false;

  public file?: File;
  private selectedFile?: IFileForUpload;

  constructor(
    private dialogRef: MatDialogRef<CreateTransactionByScanComponent>,
    private transactionService: TransactionService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public selectFile($event: NgxDropzoneChangeEvent): void {
    const files = $event.addedFiles;

    if (files?.length) {
      if (files.length > 1) {
        this.snackbarService.warning('Only one receipt can be scanned.');
        return;
      }

      const file: File = files[0];

      if (!file.type.startsWith(fileStatics.imageFiles)) {
        this.snackbarService.warning('Only images can be scanned.');
        return;
      }

      if (file.size > fileStatics.maxBytesForUpload) {
        this.snackbarService.warning(this.translationKeys.uploadFileInvalidSize);
        return;
      }

      const fileReader: FileReader = new FileReader();
      fileReader.readAsArrayBuffer(file);

      fileReader.onloadend = (readerEvent: ProgressEvent<FileReader>) => {
        if (readerEvent.target && readerEvent.target.readyState == FileReader.DONE) {
          const arrayBuffer: ArrayBuffer = readerEvent.target.result as ArrayBuffer;
          const uintArray: Uint8Array = new Uint8Array(arrayBuffer);

          this.file = file;

          this.selectedFile = {
            content: Array.from(uintArray),
            type: file.type,
            name: file.name,
            size: file.size
          };
        }
      };
    }
  }

  public removeFile() {
    this.file = undefined;
    this.selectedFile = undefined;
  }

  public createTransaction(): void {
    this.isAdding = true;
    if (this.selectedFile) {
      this.transactionService
        .createTransactionByScan(this.selectedFile)
        .pipe(take(1))
        .subscribe({
          next: () => {
            this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
            this.snackbarService.success(this.translationKeys.createTransactionSuccessful);
          },
          error: (error) => {
            this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
            this.snackbarService.showError(error);
          }
        });
    }
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }
}
