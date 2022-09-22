import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { Observable } from 'rxjs';
import { IAccountResponse } from '../models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountsApiRoute = 'accounts';

  constructor(private baseApiService: BaseApiService) {}

  public getAccounts(): Observable<IAccountResponse[]> {
    return this.baseApiService.get<IAccountResponse[]>(this.accountsApiRoute);
  }
}
