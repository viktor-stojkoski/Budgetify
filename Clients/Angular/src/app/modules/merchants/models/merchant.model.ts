export interface IMerchantResponse {
  name: string;
  categoryName: string;
}

export interface ICreateMerchantRequest {
  name: string | null;
  categoryUid: string | null;
}

export interface ICategoryResponse {
  uid: string;
  name: string;
  type: string;
}
