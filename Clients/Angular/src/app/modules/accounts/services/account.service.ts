import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { IAccountResponse } from '../models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountsApiRoute = 'accounts';

  constructor(private baseApiService: BaseApiService) {}

  public getAccounts(): Observable<IResult<IAccountResponse[]>> {
    return this.baseApiService.get<IResult<IAccountResponse[]>>(this.accountsApiRoute);
  }
}
