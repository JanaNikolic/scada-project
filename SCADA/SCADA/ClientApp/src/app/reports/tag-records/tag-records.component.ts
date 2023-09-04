import { Component, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedAlarm } from 'src/app/model/ActivatedAlarm';
import { TagRecord } from 'src/app/model/TagRecord';
import { ReportService } from 'src/app/services/ReportService/report.service';

@Component({
  selector: 'app-tag-records',
  templateUrl: './tag-records.component.html',
  styleUrls: ['./tag-records.component.css']
})
export class TagRecordsComponent {
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
    let id: number = this.inputForm.value.id ?? 0;
    this.reportService.getAllTagValues(id).subscribe({
      next: (res) => {
        console.log(res);
        this.records = [...res];
        this.dataSource = new MatTableDataSource<TagRecord>(this.records);

        this.dataSource.paginator = this.paginator;
      },
    });
  }
}
