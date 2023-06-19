import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import { IBudgetResponse } from '../models/budget.model';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private budgetsApiRoute = 'budgets';

  constructor(private baseApiService: BaseApiService) {}

  public getBudgets(): Observable<IResult<IBudgetResponse[]>> {
    return this.baseApiService.get<IResult<IBudgetResponse[]>>(this.budgetsApiRoute);
  }
}
