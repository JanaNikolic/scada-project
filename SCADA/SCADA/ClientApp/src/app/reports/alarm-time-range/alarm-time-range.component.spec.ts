import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlarmTimeRangeComponent } from './alarm-time-range.component';

describe('AlarmTimeRangeComponent', () => {
  let component: AlarmTimeRangeComponent;
  let fixture: ComponentFixture<AlarmTimeRangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlarmTimeRangeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlarmTimeRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
