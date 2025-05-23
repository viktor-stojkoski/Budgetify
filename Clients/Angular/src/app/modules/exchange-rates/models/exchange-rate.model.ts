export interface IExchangeRateResponse {
  fromCurrencyCode: string;
  toCurrencyCode: string;
  fromDate?: Date;
  toDate?: Date;
  rate: number;
}

export interface ICurrencyResponse {
  name: string;
  code: string;
  symbol?: string;
}

export interface ICreateExchangeRateRequest {
  fromCurrencyCode: string | null;
  toCurrencyCode: string | null;
  fromDate: Date | null;
  rate: number | null;
}

export interface IUpdateExchangeRateRequest {
  fromDate: Date | null;
  rate: number | null;
}
