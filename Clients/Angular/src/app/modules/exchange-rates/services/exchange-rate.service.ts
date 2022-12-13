import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { IExchangeRateResponse } from '../models/exchange-rate.model';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService {
  private exchangeRatesApiRoute = 'exchange-rates';

  constructor(private baseApiService: BaseApiService) {}

  public getExchangeRates(): Observable<IResult<IExchangeRateResponse[]>> {
    return this.baseApiService.get<IResult<IExchangeRateResponse[]>>(this.exchangeRatesApiRoute);
  }
}
