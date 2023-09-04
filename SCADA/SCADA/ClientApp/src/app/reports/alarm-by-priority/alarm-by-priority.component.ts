import { ReportService } from './../../services/ReportService/report.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedAlarm } from 'src/app/model/ActivatedAlarm';
import { TagserviceService } from 'src/app/services/TagService/tagservice.service';

@Component({
  selector: 'app-alarm-by-priority',
  templateUrl: './alarm-by-priority.component.html',
  styleUrls: ['./alarm-by-priority.component.css']
})
export class AlarmByPriorityComponent {
  displayedColumns: string[] = ['Tag Id', 'Type', 'Priority', 'Tag Value', 'Timestamp'];
  dataSource = new MatTableDataSource<ActivatedAlarm>();

  alarms: ActivatedAlarm[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private reportService: ReportService) { }
  @ViewChild('paginator') paginator!: MatPaginator;

  inputForm = new FormGroup({
    priority: new FormControl('', [Validators.required, Validators.minLength(2)]),
  });

  isFormValid(): boolean {
    return this.inputForm.valid;
  }

  getAlarms() {
    
    let priority = this.inputForm.value.priority as string; 

    this.reportService.getAlarmsByPriority(priority).subscribe({
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
