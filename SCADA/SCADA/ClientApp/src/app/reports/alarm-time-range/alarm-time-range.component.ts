import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedAlarm } from 'src/app/model/ActivatedAlarm';
import { ReportService } from 'src/app/services/ReportService/report.service';

@Component({
  selector: 'app-alarm-time-range',
  templateUrl: './alarm-time-range.component.html',
  styleUrls: ['./alarm-time-range.component.css']
})
export class AlarmTimeRangeComponent {

  displayedColumns: string[] = ['Tag Id', 'Type', 'Priority', 'Tag Value', 'Timestamp'];
  dataSource = new MatTableDataSource<ActivatedAlarm>();

  alarms: ActivatedAlarm[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private reportService: ReportService) { }
  @ViewChild('paginator') paginator!: MatPaginator;

  inputForm = new FormGroup({
    startDate: new FormControl('', Validators.required),
    endDate: new FormControl('', Validators.required),
  });

  isFormValid(): boolean {
    return this.inputForm.valid;
  }

  getAlarms() {
    const startDate = this.inputForm.get('startDate')?.value ?? "";
    const endDate = this.inputForm.get('endDate')?.value ?? "";


    this.reportService.getAlarmsByRange(startDate, endDate).subscribe({
      next: (res) => {
        if (res.length === 0) {
          this.snackBar.open('There have been no alarms of this priority', "", {duration: 2000});
        }
        this.alarms = [...res];
        this.dataSource = new MatTableDataSource<ActivatedAlarm>(this.alarms);
        this.dataSource.paginator = this.paginator;
      }
    })
  }
}
