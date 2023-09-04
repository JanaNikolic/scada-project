import {AfterViewInit, Component, ViewChild} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { TagRecord } from 'src/app/model/TagRecord';
import { ReportService } from 'src/app/services/ReportService/report.service';
import {MatSort} from "@angular/material/sort";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-records-time-range',
  templateUrl: './records-time-range.component.html',
  styleUrls: ['./records-time-range.component.css']
})
export class RecordsTimeRangeComponent implements AfterViewInit{
  displayedColumns: string[] = ['I/O Address', 'Timestamp', 'Tag Value'];
  dataSource = new MatTableDataSource<TagRecord>();

  records: TagRecord[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private reportService: ReportService, private datePipe: DatePipe) { }
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

  getRecords() {
    if (this.inputForm.valid){
      const { startDate, endDate } = this.inputForm.value;

      const start = this.datePipe.transform(startDate, 'yyyy-MM-ddTHH:mm:ss');
      const end = this.datePipe.transform(endDate, 'yyyy-MM-ddTHH:mm:ss');
      console.log(start);

      this.reportService.getRecordsByRange(start, end).subscribe({
        next: (res) => {
          if (res.length === 0) {
            this.snackBar.open('There have been no tag records within this date range', "", {duration: 2000});
          }
          this.records = [...res];
          console.log(this.records);
          this.dataSource = new MatTableDataSource(this.records);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      })
    }
  }
}
