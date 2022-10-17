import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import {
  IAccountResponse,
  ICreateAccountRequest,
  ICurrencyResponse,
  IUpdateAccountRequest
} from '../models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountsApiRoute = 'accounts';
  private currenciesApiRoute = 'currencies';

  constructor(private baseApiService: BaseApiService) {}

  public getAccounts(): Observable<IResult<IAccountResponse[]>> {
    return this.baseApiService.get<IResult<IAccountResponse[]>>(this.accountsApiRoute);
  }

  public getAccount(uid: string | null): Observable<IResult<IAccountResponse>> {
    return this.baseApiService.get<IResult<IAccountResponse>>(`${this.accountsApiRoute}/${uid}`);
  }

  public createAccount(request: ICreateAccountRequest): Observable<void> {
    return this.baseApiService.post<void>(this.accountsApiRoute, request);
  }

  public updateAccount(uid: string | null, request: IUpdateAccountRequest): Observable<void> {
    return this.baseApiService.put<void>(`${this.accountsApiRoute}/${uid}`, request);
  }

  public getCurrencies(): Observable<IResult<ICurrencyResponse[]>> {
    return this.baseApiService.get<IResult<ICurrencyResponse[]>>(this.currenciesApiRoute);
  }
}
