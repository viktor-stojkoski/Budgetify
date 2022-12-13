import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { ICreateExchangeRateRequest, ICurrencyResponse, IExchangeRateResponse } from '../models/exchange-rate.model';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService {
  private exchangeRatesApiRoute = 'exchange-rates';
  private currenciesApiRoute = 'currencies';

  constructor(private baseApiService: BaseApiService) {}

  public getExchangeRates(): Observable<IResult<IExchangeRateResponse[]>> {
    return this.baseApiService.get<IResult<IExchangeRateResponse[]>>(this.exchangeRatesApiRoute);
  }

  public createExchangeRate(request: ICreateExchangeRateRequest): Observable<void> {
    return this.baseApiService.post<void>(this.exchangeRatesApiRoute, request);
  }

  public getCurrencies(): Observable<IResult<ICurrencyResponse[]>> {
    return this.baseApiService.get<IResult<ICurrencyResponse[]>>(this.currenciesApiRoute);
  }
}
