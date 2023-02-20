import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IFileForUpload, IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import {
  IAccountResponse,
  IAddTransactionAttachmentsRequest,
  ICategoryResponse,
  ICreateTransactionRequest,
  ICurrencyResponse,
  IMerchantResponse,
  ITransactionDetailsResponse,
  ITransactionResponse,
  IUpdateTransactionRequest
} from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private transactionsApiRoute = 'transactions';
  private accountsApiRoute = 'accounts';
  private categoriesApiRoute = 'categories';
  private currenciesApiRoute = 'currencies';
  private merchantsApiRoute = 'merchants';

  constructor(private baseApiService: BaseApiService) {}

  public getTransactions(): Observable<IResult<ITransactionResponse[]>> {
    return this.baseApiService.get<IResult<ITransactionResponse[]>>(this.transactionsApiRoute);
  }

  public getTransaction(uid: string | null): Observable<IResult<ITransactionDetailsResponse>> {
    return this.baseApiService.get<IResult<ITransactionDetailsResponse>>(`${this.transactionsApiRoute}/${uid}`);
  }

  public createTransaction(request: ICreateTransactionRequest): Observable<void> {
    return this.baseApiService.post<void>(this.transactionsApiRoute, request);
  }

  public createTransactionByScan(request: IFileForUpload): Observable<void> {
    return this.baseApiService.post<void>(`${this.transactionsApiRoute}/create-by-scan`, request);
  }

  public updateTransaction(uid: string | null, request: IUpdateTransactionRequest): Observable<void> {
    return this.baseApiService.put<void>(`${this.transactionsApiRoute}/${uid}`, request);
  }

  public verifyTransaction(uid: string | null): Observable<void> {
    return this.baseApiService.patch<void>(`${this.transactionsApiRoute}/${uid}/verify`);
  }

  public deleteTransaction(uid: string | null): Observable<void> {
    return this.baseApiService.delete<void>(`${this.transactionsApiRoute}/${uid}`);
  }

  public addTransactionAttachments(
    transactionUid: string,
    request: IAddTransactionAttachmentsRequest
  ): Observable<void> {
    return this.baseApiService.post<void>(`${this.transactionsApiRoute}/${transactionUid}/attachments`, request);
  }

  public deleteTransactionAttachment(transactionUid: string, attachmentUid: string): Observable<void> {
    return this.baseApiService.delete<void>(
      `${this.transactionsApiRoute}/${transactionUid}/attachments/${attachmentUid}`
    );
  }

  public getAccounts(): Observable<IResult<IAccountResponse[]>> {
    return this.baseApiService.get<IResult<IAccountResponse[]>>(this.accountsApiRoute);
  }

  public getCategories(): Observable<IResult<ICategoryResponse[]>> {
    return this.baseApiService.get<IResult<ICategoryResponse[]>>(this.categoriesApiRoute);
  }

  public getCurrencies(): Observable<IResult<ICurrencyResponse[]>> {
    return this.baseApiService.get<IResult<ICurrencyResponse[]>>(this.currenciesApiRoute);
  }

  public getMerchants(): Observable<IResult<IMerchantResponse[]>> {
    return this.baseApiService.get<IResult<IMerchantResponse[]>>(this.merchantsApiRoute);
  }
}
