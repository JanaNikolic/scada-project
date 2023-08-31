import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnalogInputsComponent } from './analog-inputs.component';

describe('AnalogInputsComponent', () => {
  let component: AnalogInputsComponent;
  let fixture: ComponentFixture<AnalogInputsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AnalogInputsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnalogInputsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
