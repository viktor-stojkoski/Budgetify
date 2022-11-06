import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@budgetify/core';
import { DialogService, SharedModule } from '@budgetify/shared';
import { CreateMerchantComponent } from './components/create-merchant/create-merchant.component';
import { MerchantDetailsComponent } from './components/merchant-details/merchant-details.component';
import { MerchantsTableComponent } from './components/merchants-table/merchants-table.component';
import { routes } from './routes';
import { DeleteMerchantComponent } from './components/delete-merchant/delete-merchant.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    CoreModule,
    SharedModule,
    MatToolbarModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatAutocompleteModule,
    MatCardModule
  ],
  declarations: [MerchantsTableComponent, CreateMerchantComponent, MerchantDetailsComponent, DeleteMerchantComponent],
  providers: [DialogService]
})
export class MerchantsModule {}
