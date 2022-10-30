import { Route } from '@angular/router';
import { CategoriesTableComponent } from './components/categories-table/categories-table.component';

export const routes: Route[] = [
  {
    path: '',
    component: CategoriesTableComponent
  }
];
