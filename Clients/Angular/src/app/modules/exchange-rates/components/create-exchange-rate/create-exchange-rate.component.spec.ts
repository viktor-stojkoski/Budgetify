import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '../../../../app.module';
import { ExchangeRatesModule } from '../../exchange-rates.module';

import { CreateExchangeRateComponent } from './create-exchange-rate.component';

describe('CreateExchangeRateComponent', () => {
  let component: CreateExchangeRateComponent;
  let fixture: ComponentFixture<CreateExchangeRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExchangeRatesModule, AppModule],
      declarations: [CreateExchangeRateComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateExchangeRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
