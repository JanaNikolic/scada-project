import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TagRecordsComponent } from './tag-records.component';

describe('TagRecordsComponent', () => {
  let component: TagRecordsComponent;
  let fixture: ComponentFixture<TagRecordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TagRecordsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TagRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
