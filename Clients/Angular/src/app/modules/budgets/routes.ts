import { Route } from '@angular/router';
import { BudgetsTableComponent } from './components/budgets-table/budgets-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: BudgetsTableComponent
  }
];
