import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { ITransactionResponse } from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private transactionsApiRoute = 'transactions';

  constructor(private baseApiService: BaseApiService) {}

  public getTransactions(): Observable<IResult<ITransactionResponse[]>> {
    return this.baseApiService.get<IResult<ITransactionResponse[]>>(this.transactionsApiRoute);
  }
}
