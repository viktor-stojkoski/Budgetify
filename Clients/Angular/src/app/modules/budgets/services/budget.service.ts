import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { IBudgetResponse, ICategoryResponse, ICreateBudgetRequest, IUpdateBudgetRequest } from '../models/budget.model';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private budgetsApiRoute = 'budgets';
  private categoriesApiRoute = 'categories';

  constructor(private baseApiService: BaseApiService) {}

  public getBudgets(): Observable<IResult<IBudgetResponse[]>> {
    return this.baseApiService.get<IResult<IBudgetResponse[]>>(this.budgetsApiRoute);
  }

  public getBudget(uid: string | null): Observable<IResult<IBudgetResponse>> {
    return this.baseApiService.get<IResult<IBudgetResponse>>(`${this.budgetsApiRoute}/${uid}`);
  }

  public createBudget(request: ICreateBudgetRequest): Observable<void> {
    return this.baseApiService.post<void>(this.budgetsApiRoute, request);
  }

  public updateBudget(uid: string | null, request: IUpdateBudgetRequest): Observable<void> {
    return this.baseApiService.put<void>(`${this.budgetsApiRoute}/${uid}`, request);
  }

  public deleteBudget(uid: string | null): Observable<void> {
    return this.baseApiService.delete<void>(`${this.budgetsApiRoute}/${uid}`);
  }

  public getCategories(): Observable<IResult<ICategoryResponse[]>> {
    return this.baseApiService.get<IResult<ICategoryResponse[]>>(this.categoriesApiRoute);
  }
}
