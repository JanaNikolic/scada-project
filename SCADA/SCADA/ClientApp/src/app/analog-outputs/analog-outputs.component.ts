import { AnalogInput } from './../model/AnalogInput';
import { TagserviceService } from './../services/TagService/tagservice.service';
import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AnalogOutput } from '../model/AnalogOutput';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { OutputDialogComponent } from '../dialogs/output-dialog/output-dialog.component';

@Component({
  selector: 'app-analog-outputs',
  templateUrl: './analog-outputs.component.html',
  styleUrls: ['./analog-outputs.component.css']
})
export class AnalogOutputsComponent {
  displayedColumns: string[] = ['select', 'Id', 'Description', 'I/O Address', 'Initial value', 'Low limit', 'High limit', 'Units', 'Current Value'];
  dataSource = new MatTableDataSource<AnalogOutput>();
  selection = new SelectionModel<AnalogOutput>(true, []);

  inputTags: AnalogOutput[] = [];
  constructor(private snackBar: MatSnackBar, private tagService: TagserviceService, private matDialog: MatDialog) { }
  @ViewChild('paginator') paginator!: MatPaginator;

  ngOnInit() {

    this.tagService.getOutputTags().subscribe({
      next: (res) => {
        console.log(res);
        this.inputTags = [...res.analogOutputList, ...res.digitalOutputList];
        this.dataSource = new MatTableDataSource<AnalogOutput>(this.inputTags);
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
    let dialogRef: MatDialogRef<OutputDialogComponent>;

    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = "300px";

    dialogRef = this.matDialog.open(OutputDialogComponent, dialogConfig);
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
}
