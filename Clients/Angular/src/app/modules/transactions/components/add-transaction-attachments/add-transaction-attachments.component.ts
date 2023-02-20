import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  DestroyBaseComponent,
  DialogActionButton,
  IDialogResponseData,
  IFileForUpload,
  SnackbarService,
  TranslationKeys as SharedTranslationKeys
} from '@budgetify/shared';
import { NgxDropzoneChangeEvent } from 'ngx-dropzone/public_api';
import { take } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { fileStatics } from '../../static/fileStatics';
import { TranslationKeys } from '../../static/translationKeys';

@Component({
  selector: 'app-add-transaction-attachments',
  templateUrl: './add-transaction-attachments.component.html',
  styleUrls: ['./add-transaction-attachments.component.scss']
})
export class AddTransactionAttachmentsComponent extends DestroyBaseComponent {
  public readonly translationKeys = TranslationKeys;
  public readonly sharedTranslationKeys = SharedTranslationKeys;
  public isAdding = false;

  public nonImages: File[] = [];
  public images: File[] = [];
  private selectedFiles: IFileForUpload[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public transactionUid: string,
    private dialogRef: MatDialogRef<AddTransactionAttachmentsComponent>,
    private transactionService: TransactionService,
    private snackbarService: SnackbarService
  ) {
    super();
  }

  public selectFiles($event: NgxDropzoneChangeEvent): void {
    const files = $event.addedFiles;

    if (files?.length) {
      for (let i = 0; i < files.length; i++) {
        const file: File = files[i];
        if (file.size > fileStatics.maxBytesForUpload) {
          this.snackbarService.warning(this.translationKeys.uploadFileInvalidSize);
        } else {
          const fileReader: FileReader = new FileReader();
          fileReader.readAsArrayBuffer(file);

          fileReader.onloadend = (readerEvent: ProgressEvent<FileReader>) => {
            if (readerEvent.target && readerEvent.target.readyState == FileReader.DONE) {
              const arrayBuffer: ArrayBuffer = readerEvent.target.result as ArrayBuffer;
              const uintArray: Uint8Array = new Uint8Array(arrayBuffer);

              if (file.type.match(fileStatics.imageFiles)) {
                this.images.push(file);
              } else {
                this.nonImages.push(file);
              }

              this.selectedFiles.push({
                content: Array.from(uintArray),
                type: file.type,
                name: file.name,
                size: file.size
              });
            }
          };
        }
      }
    }
  }

  public removeFile(file: File) {
    if (file.type.match(fileStatics.imageFiles)) {
      this.images.splice(this.images.indexOf(file), 1);
    } else {
      this.nonImages.splice(this.nonImages.indexOf(file), 1);
    }
    this.selectedFiles = this.selectedFiles.filter((x) => x.name !== file.name);
  }

  public addTransactionAttachments(): void {
    this.isAdding = true;
    this.transactionService
      .addTransactionAttachments(this.transactionUid, {
        attachments: this.selectedFiles
      })
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.dialogRef.close({ action: DialogActionButton.Ok } as IDialogResponseData);
          this.snackbarService.success(this.translationKeys.addTransactionAttachmentsSuccessful);
        },
        error: (error) => {
          this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
          this.snackbarService.showError(error);
        }
      });
  }

  public onCancelClick(): void {
    this.dialogRef.close({ action: DialogActionButton.Cancel } as IDialogResponseData);
  }
}
