export interface IExchangeRateResponse {
  fromCurrencyCode: string;
  toCurrencyCode: string;
  fromDate?: Date;
  toDate?: Date;
  rate: number;
}
