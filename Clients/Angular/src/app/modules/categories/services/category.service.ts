import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { ICategoryResponse, ICreateCategoryRequest, IUpdateCategoryRequest } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private categoriesApiRoute = 'categories';

  constructor(private baseApiService: BaseApiService) {}

  public getCategories(): Observable<IResult<ICategoryResponse[]>> {
    return this.baseApiService.get<IResult<ICategoryResponse[]>>(this.categoriesApiRoute);
  }

  public getCategory(uid: string | null): Observable<IResult<ICategoryResponse>> {
    return this.baseApiService.get<IResult<ICategoryResponse>>(`${this.categoriesApiRoute}/${uid}`);
  }

  public createCategory(request: ICreateCategoryRequest): Observable<void> {
    return this.baseApiService.post<void>(this.categoriesApiRoute, request);
  }

  public updateCategory(uid: string | null, request: IUpdateCategoryRequest): Observable<void> {
    return this.baseApiService.put<void>(`${this.categoriesApiRoute}/${uid}`, request);
  }

  public deleteCategory(uid: string | null): Observable<void> {
    return this.baseApiService.delete<void>(`${this.categoriesApiRoute}/${uid}`);
  }
}
