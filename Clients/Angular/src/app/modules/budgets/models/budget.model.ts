export interface IBudgetResponse {
  uid: string;
  name: string;
  categoryName: string;
  startDate: Date;
  endDate: Date;
  amount: number;
  amountSpent: number;
}

export interface ICreateBudgetRequest {
  name: string | null;
  categoryUid: string | null;
  startDate: Date | null;
  endDate: Date | null;
  amount: number | null;
  amountSpent: number | null;
}

export interface IUpdateBudgetRequest {
  name: string | null;
  amount: number | null;
}

export interface ICategoryResponse {
  uid: string;
  name: string;
  type: string;
}

export interface IDeleteBudgetDialogData {
  uid: string;
  name: string;
}
