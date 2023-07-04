import { Route } from '@angular/router';
import { BudgetDetailsComponent } from './components/budget-details/budget-details.component';
import { BudgetsTableComponent } from './components/budgets-table/budgets-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: BudgetsTableComponent
  },
  {
    path: ':budgetUid',
    component: BudgetDetailsComponent
  }
];
