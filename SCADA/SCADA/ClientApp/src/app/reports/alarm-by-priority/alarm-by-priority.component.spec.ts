import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlarmByPriorityComponent } from './alarm-by-priority.component';

describe('AlarmByPriorityComponent', () => {
  let component: AlarmByPriorityComponent;
  let fixture: ComponentFixture<AlarmByPriorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlarmByPriorityComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlarmByPriorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
