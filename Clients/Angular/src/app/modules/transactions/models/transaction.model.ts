export interface ITransactionResponse {
  accountName: string;
  categoryName: string;
  currencyCode: string;
  merchantName?: string;
  type: string;
  amount: number;
  date: string; // TODO: DATE,
  description: string | null;
}
