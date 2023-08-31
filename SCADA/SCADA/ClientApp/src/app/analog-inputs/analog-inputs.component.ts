import { AnalogInput } from './../model/AnalogInput';
import { TagserviceService } from './../services/TagService/tagservice.service';
import { Component, ViewChild } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {SelectionModel} from '@angular/cdk/collections';
import {MatSnackBar} from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { InputDialogComponent } from '../dialogs/input-dialog/input-dialog/input-dialog.component';
import { OutputDialogComponent } from '../dialogs/output-dialog/output-dialog.component';
import { ValueDialogComponent } from '../dialogs/value-dialog/value-dialog/value-dialog.component';


@Component({
  selector: 'app-analog-inputs',
  templateUrl: './analog-inputs.component.html',
  styleUrls: ['./analog-inputs.component.css']
})
export class AnalogInputsComponent {

  displayedColumns: string[] = ['select', 'Id', 'Description', 'Driver', 'I/O Address', 'Scan time', 'Low limit', 'High limit', 'Units', 'Scan'];
  dataSource = new MatTableDataSource<AnalogInput>();
  selection = new SelectionModel<AnalogInput>(true, []);

  inputTags: AnalogInput[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private tagService: TagserviceService) { }
  @ViewChild('paginator') paginator!: MatPaginator;

  ngOnInit() {

    this.tagService.getInputTags().subscribe({
      next: (res) => {
        console.log(res);
        this.inputTags = [...res.analogInputList, ...res.digitalInputList];
        this.dataSource = new MatTableDataSource<AnalogInput>(this.inputTags);
        this.dataSource.paginator = this.paginator;
      },
    });
  }


  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  openForm() {
    let dialogRef: MatDialogRef<InputDialogComponent>;

    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = "300px";

    dialogRef = this.matDialog.open(InputDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(() => {
      
    });
  }

  deleteTag() {
    this.selection.selected.forEach(tag => {
      let id = tag.id;
      this.tagService.deleteTag(id).subscribe({
        next: (res) => {
          this.inputTags = this.inputTags.filter(item => item.id !== id);
          // Update the dataSource with the filtered inputTags array
          this.dataSource.data = this.inputTags;

          this.snackBar.open("Successfuly deleted", 'Close', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });
        },
        error:(error)=>{
          this.snackBar.open('Deletion was unsuccessful', 'Close', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });
        }
      });
    })
  }

  toggleScan(id: number) {
    this.tagService.toggleScan(id).subscribe({
      next: (res) => {
        this.snackBar.open("Successful", 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
        });
      },
      error:(error)=>{
        this.snackBar.open('Toggle was unsuccessful', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
        });
      }
    });
  }

}