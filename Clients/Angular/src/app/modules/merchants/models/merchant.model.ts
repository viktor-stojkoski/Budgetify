export interface IMerchantResponse {
  uid: string;
  name: string;
  categoryName: string;
}

export interface ICreateMerchantRequest {
  name: string | null;
  categoryUid: string | null;
}

export interface IUpdateMerchantRequest {
  name: string | null;
  categoryUid: string | null;
}

export interface ICategoryResponse {
  uid: string;
  name: string;
  type: string;
}

export interface IDeleteMerchantDialogData {
  name: string;
  uid: string;
}
