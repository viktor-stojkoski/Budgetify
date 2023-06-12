export interface IBudgetResponse {
  uid: string;
  name: string;
  categoryName: string;
  startDate: Date;
  endDate: Date;
  amount: number;
  amountSpent: number;
}
