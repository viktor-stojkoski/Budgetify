import { IFileForUpload } from '@budgetify/shared';

export interface ITransactionResponse {
  accountUid?: string;
  accountName?: string;
  categoryUid?: string;
  categoryName?: string;
  currencyCode: string;
  merchantUid?: string;
  merchantName?: string;
  type: string;
  amount: number;
  date?: Date;
  description: string | null;
  isVerified: boolean;
}

export interface ITransactionDetailsResponse extends ITransactionResponse {
  transactionAttachments?: ITransactionAttachmentResponse[];
}

export interface ITransactionAttachmentResponse {
  uid: string;
  createdOn: Date;
  name: string;
  url: string;
}

export interface ICurrencyResponse {
  name: string;
  code: string;
  symbol?: string;
}

export interface IAccountResponse {
  uid: string;
  name: string;
}

export interface ICategoryResponse {
  uid: string;
  name: string;
  type: string;
}

export interface IMerchantResponse {
  uid: string;
  name: string;
  categoryName: string;
}

export interface ICreateTransactionRequest {
  accountUid: string | null;
  fromAccountUid: string | null;
  categoryUid: string | null;
  currencyCode: string | null;
  merchantUid: string | null;
  type: string | null;
  amount: number | null;
  date: Date | null;
  description: string | null;
  attachments: IFileForUpload[];
}

export interface IUpdateTransactionRequest {
  accountUid: string | null;
  categoryUid: string | null;
  currencyCode: string | null;
  merchantUid: string | null;
  amount: number | null;
  date: Date | null;
  description: string | null;
}

export interface IAddTransactionAttachmentsRequest {
  attachments: IFileForUpload[];
}

export interface IDeleteTransactionDialogData {
  uid: string;
}

export interface IDeleteTransactionAttachmentDialogData {
  transactionUid: string;
  attachmentUid: string;
  name: string;
}

export interface IVerifyTransactionDialogData {
  uid: string;
}
