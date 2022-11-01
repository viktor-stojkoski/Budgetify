import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '../../../../app.module';
import { MerchantsModule } from '../../merchants.module';

import { MerchantsTableComponent } from './merchants-table.component';

describe('MerchantsTableComponent', () => {
  let component: MerchantsTableComponent;
  let fixture: ComponentFixture<MerchantsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MerchantsModule, AppModule],
      declarations: [MerchantsTableComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(MerchantsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
