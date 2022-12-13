import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateExchangeRateComponent } from './create-exchange-rate.component';

describe('CreateExchangeRateComponent', () => {
  let component: CreateExchangeRateComponent;
  let fixture: ComponentFixture<CreateExchangeRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateExchangeRateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateExchangeRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
