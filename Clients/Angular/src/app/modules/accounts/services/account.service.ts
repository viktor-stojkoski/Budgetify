import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { IAccountRequest, IAccountResponse, ICurrencyResponse } from '../models/account.model';

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

  public getAccount(uid?: string | null): Observable<IResult<IAccountResponse>> {
    return this.baseApiService.get<IResult<IAccountResponse>>(`${this.accountsApiRoute}/${uid}`);
  }

  public createAccount(request: IAccountRequest): Observable<void> {
    return this.baseApiService.post<void>(this.accountsApiRoute, request);
  }

  public getCurrencies(): Observable<IResult<ICurrencyResponse[]>> {
    return this.baseApiService.get<IResult<ICurrencyResponse[]>>(this.currenciesApiRoute);
  }
}
