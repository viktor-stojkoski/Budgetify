export interface IAccountResponse {
  name: string;
  type: string;
  balance: number;
  currencyCode: string;
  description: string | null;
}

export interface ICreateAccountRequest {
  name: string | null;
  type: string | null;
  balance: number | null;
  currencyCode: string | null;
  description: string | null;
}

export interface IUpdateAccountRequest {
  name: string | null;
  type: string | null;
  balance: number | null;
  currencyCode: string | null;
  description: string | null;
}

export interface ICurrencyResponse {
  name: string;
  code: string;
  symbol?: string;
}

export interface IDeleteAccountDialogData {
  name: string;
  uid: string;
}
