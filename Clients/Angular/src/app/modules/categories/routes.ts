import { Route } from '@angular/router';
import { CategoriesTableComponent } from './components/categories-table/categories-table.component';
import { CategoryDetailsComponent } from './components/category-details/category-details.component';

export const routes: Route[] = [
  {
    path: '',
    component: CategoriesTableComponent
  },
  {
    path: ':categoryUid',
    component: CategoryDetailsComponent
  }
];
