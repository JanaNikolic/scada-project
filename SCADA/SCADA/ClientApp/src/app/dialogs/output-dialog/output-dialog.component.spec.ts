import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutputDialogComponent } from './output-dialog.component';

describe('OutputDialogComponent', () => {
  let component: OutputDialogComponent;
  let fixture: ComponentFixture<OutputDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OutputDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OutputDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
