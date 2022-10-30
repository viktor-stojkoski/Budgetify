import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '../../../../app.module';
import { CategoriesModule } from '../../categories.module';

import { CategoriesTableComponent } from './categories-table.component';

describe('CategoriesTableComponent', () => {
  let component: CategoriesTableComponent;
  let fixture: ComponentFixture<CategoriesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategoriesModule, AppModule],
      declarations: [CategoriesTableComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(CategoriesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
