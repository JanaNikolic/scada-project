import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordsTimeRangeComponent } from './records-time-range.component';

describe('RecordsTimeRangeComponent', () => {
  let component: RecordsTimeRangeComponent;
  let fixture: ComponentFixture<RecordsTimeRangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecordsTimeRangeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecordsTimeRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
