export interface IAccountResponse {
  name: string;
  type: string;
  balance: number;
  description?: string;
}

export interface IAccountRequest {
  name: string | null;
  type: string | null;
  balance: number | null;
  currencyCode: string | null;
  description: string | null;
}
