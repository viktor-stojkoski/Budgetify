import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppModule } from '../../../../app.module';
import { BudgetsModule } from '../../budgets.module';
import { BudgetsTableComponent } from './budgets-table.component';

describe('BudgetsTableComponent', () => {
  let component: BudgetsTableComponent;
  let fixture: ComponentFixture<BudgetsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BudgetsModule, AppModule],
      declarations: [BudgetsTableComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(BudgetsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
