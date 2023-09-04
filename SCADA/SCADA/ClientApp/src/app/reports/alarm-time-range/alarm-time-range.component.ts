import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedAlarm } from 'src/app/model/ActivatedAlarm';
import { ReportService } from 'src/app/services/ReportService/report.service';
import {DatePipe} from "@angular/common";
import {MatSort} from "@angular/material/sort";

@Component({
  selector: 'app-alarm-time-range',
  templateUrl: './alarm-time-range.component.html',
  styleUrls: ['./alarm-time-range.component.css']
})
export class AlarmTimeRangeComponent implements AfterViewInit{

  displayedColumns: string[] = ['Tag Id', 'Type', 'Priority', 'Threshold', 'Timestamp'];
  dataSource = new MatTableDataSource<ActivatedAlarm>();

  alarms: ActivatedAlarm[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private reportService: ReportService, private datePipe: DatePipe) { this.dataSource = new MatTableDataSource(); }
  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;

    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
  }
  inputForm = new FormGroup({
    startDate: new FormControl(new Date(), Validators.required),
    endDate: new FormControl(new Date(), Validators.required),
  });

  isFormValid(): boolean {
    return this.inputForm.valid;
  }

  getAlarms() {
    if (this.inputForm.valid){
      const { startDate, endDate } = this.inputForm.value;

      const start = this.datePipe.transform(startDate, 'yyyy-MM-ddTHH:mm:ss');
      const end = this.datePipe.transform(endDate, 'yyyy-MM-ddTHH:mm:ss');
      console.log(start);

      this.reportService.getAlarmsByRange(start, end).subscribe({
        next: (res) => {
          if (res.length === 0) {
            this.snackBar.open('There have been no alarms within this date range', "", {duration: 2000});
          }
          this.alarms = [...res];
          this.dataSource = new MatTableDataSource(this.alarms);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      })
    }
  }
}
