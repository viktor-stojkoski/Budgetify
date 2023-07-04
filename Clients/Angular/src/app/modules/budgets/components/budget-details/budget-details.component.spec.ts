import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppModule } from '../../../../app.module';
import { BudgetsModule } from '../../budgets.module';
import { BudgetDetailsComponent } from './budget-details.component';

describe('BudgetDetailsComponent', () => {
  let component: BudgetDetailsComponent;
  let fixture: ComponentFixture<BudgetDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BudgetsModule, AppModule],
      declarations: [BudgetDetailsComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(BudgetDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
