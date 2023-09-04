import { SelectionModel } from '@angular/cdk/collections';
import { Component, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MatSort, Sort} from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AnalogInput } from 'src/app/model/AnalogInput';
import { ReportService } from 'src/app/services/ReportService/report.service';

@Component({
  selector: 'app-input-report',
  templateUrl: './input-report.component.html',
  styleUrls: ['./input-report.component.css']
})
export class InputReportComponent {
  displayedColumns: string[] = ['Id', 'Description', 'Driver', 'I/O Address', 'Scan time', 'Low limit', 'High limit', 'Units', 'Current Value'];
  dataSource = new MatTableDataSource<AnalogInput>();
  selection = new SelectionModel<AnalogInput>(true, []);

  inputTags: AnalogInput[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private reportService: ReportService) { }
  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild('empTbSort') empTbSort = new MatSort();

  inputForm = new FormGroup({
    type: new FormControl('', [Validators.required, Validators.minLength(2)]),
  });

  ngAfterViewInit() {    
    this.dataSource.sort = this.empTbSort;
  }

  isFormValid(): boolean {
    return this.inputForm.valid;
  }
  getTags() {

    this.reportService.getAllcurrentInputValues().subscribe({
      next: (res) => {
        console.log(res.analogInputList);
        if (this.inputForm.value.type as string === "ANALOG") {
          this.inputTags = [...res.analogInputList];
          this.dataSource = new MatTableDataSource<AnalogInput>(this.inputTags);
          this.dataSource.sort = this.empTbSort; 
        }
        else {
          this.inputTags = [...res.digitalInputList];
          this.dataSource = new MatTableDataSource<AnalogInput>(this.inputTags);
          this.dataSource.sort = this.empTbSort; 
        }

        this.dataSource.paginator = this.paginator;
      },
    });
  }
}
