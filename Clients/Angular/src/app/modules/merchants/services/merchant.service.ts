import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { IMerchantResponse } from '../models/merchant.model';

@Injectable({
  providedIn: 'root'
})
export class MerchantService {
  private merchantsApiRoute = 'merchants';

  constructor(private baseApiService: BaseApiService) {}

  public getMerchants(): Observable<IResult<IMerchantResponse[]>> {
    return this.baseApiService.get<IResult<IMerchantResponse[]>>(this.merchantsApiRoute);
  }
}
