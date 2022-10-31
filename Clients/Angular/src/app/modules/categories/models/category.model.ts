export interface ICategoryResponse {
  name: string;
  type: string;
}

export interface ICreateCategoryRequest {
  name: string | null;
  type: string | null;
}

export interface IUpdateCategoryRequest {
  name: string | null;
  type: string | null;
}
