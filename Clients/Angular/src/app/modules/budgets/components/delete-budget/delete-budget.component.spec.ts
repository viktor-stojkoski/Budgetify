import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppModule } from '../../../../app.module';
import { BudgetsModule } from '../../budgets.module';
import { DeleteBudgetComponent } from './delete-budget.component';

describe('DeleteBudgetComponent', () => {
  let component: DeleteBudgetComponent;
  let fixture: ComponentFixture<DeleteBudgetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BudgetsModule, AppModule],
      declarations: [DeleteBudgetComponent],
      providers: [{ provide: MAT_DIALOG_DATA, useValue: {} }]
    }).compileComponents();

    fixture = TestBed.createComponent(DeleteBudgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
