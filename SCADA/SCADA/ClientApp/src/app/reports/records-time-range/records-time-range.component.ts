import { Component, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { TagRecord } from 'src/app/model/TagRecord';
import { ReportService } from 'src/app/services/ReportService/report.service';

@Component({
  selector: 'app-records-time-range',
  templateUrl: './records-time-range.component.html',
  styleUrls: ['./records-time-range.component.css']
})
export class RecordsTimeRangeComponent {
  displayedColumns: string[] = ['I/O Address', 'Timestamp', 'Tag Value'];
  dataSource = new MatTableDataSource<TagRecord>();

  records: TagRecord[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private reportService: ReportService) { }
  @ViewChild('paginator') paginator!: MatPaginator;

  inputForm = new FormGroup({
    id: new FormControl(0, [Validators.required]),
  });

  isFormValid(): boolean {
    return this.inputForm.valid;
  }

  getRecords() {
    
  }
}
