import { AnalogInput } from './../model/AnalogInput';
import { TagserviceService } from './../services/TagService/tagservice.service';
import { Component, ViewChild } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {SelectionModel} from '@angular/cdk/collections';
import {MatSnackBar} from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { InputDialogComponent } from '../dialogs/input-dialog/input-dialog/input-dialog.component';
import {Alarm, InputsDTO} from "../model/Alarm";
import {AlarmComponent} from "../dialogs/alarm/alarm.component";
import {Router} from "@angular/router";
import {DigitalInput} from "../model/DigitalInput";


@Component({
  selector: 'app-analog-inputs',
  templateUrl: './analog-inputs.component.html',
  styleUrls: ['./analog-inputs.component.css']
})
export class AnalogInputsComponent {

  displayedColumns: string[] = ['select', 'Id', 'Description', 'Driver', 'I/O Address', 'Scan time', 'Low limit', 'High limit', 'Units', 'Scan', 'Alarms'];
  dataSource = new MatTableDataSource<AnalogInput>();
  selection = new SelectionModel<AnalogInput>(true, []);

  inputTags: AnalogInput[] = [];
  constructor(private snackBar: MatSnackBar, private matDialog: MatDialog, private tagService: TagserviceService, private router: Router) { this.connectHub();}
  @ViewChild('paginator') paginator!: MatPaginator;
  isSelectedAlarm: boolean = false;
  selectedAlarm: Alarm | undefined;
  tags: any[] = [];

  ngOnInit() {

    this.tagService.getInputTags().subscribe({
      next: (res) => {
        console.log(res);
        this.inputTags = [...res.analogInputList, ...res.digitalInputList];
        res.analogInputList.forEach((x: any) => this.tags.push(x));
        res.digitalInputList.forEach((x: any) => this.tags.push(x));
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
      this.ngOnInit();
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

  isAlarmActive(alarm: Alarm, input: AnalogInput) : boolean {
    if(!input.isScanOn) return false
    if(alarm.type == 0 && input.value < alarm.threshold) return true
    if(alarm.type == 1 && input.value > alarm.threshold) return true
    return false
  }

  addAlarm(element : AnalogInput) {
    let dialogRef: MatDialogRef<AlarmComponent>;

    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = element;
    dialogConfig.width = "500px";

    dialogRef = this.matDialog.open(AlarmComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((result) => {
      this.ngOnInit();
    });
  }

  deleteAlarm() {
    if (this.selectedAlarm){
      console.log(this.selectedAlarm.id);
      this.tagService.deleteAlarm(this.selectedAlarm.id).subscribe({
        next: (res) => {
          this.snackBar.open("Successfully deleted alarm!", 'Close', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });
          this.ngOnInit();
        },
        error:(error)=>{
          this.snackBar.open('Something went wrong!', 'Close', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });
        }
      });
    }

  }

  selectAlarm(alarm: Alarm) {
    console.log("Out: ", this.selectedAlarm);
    if (this.selectedAlarm?.id == alarm.id) {
      this.selectedAlarm = undefined;
      this.isSelectedAlarm = false
      console.log("1: " , this.selectedAlarm);
    } else {
      this.selectedAlarm = alarm;
      this.isSelectedAlarm = true;
      console.log("2: " , this.selectedAlarm);
    }
  }

  ngOnDestroy(){
    this.tagService.stopConnection();
  }

  connectHub(){
    this.tagService.startConnection()
      .then(() => {
        console.log('SignalR Simulation connection established');
      })
      .catch((error: any) => {
        console.error('>>>SignalR Simulation connection error:', error);
      });

    this.tagService.getHubConnection().on('SendSimulationData', (data: any) => {
      this.handleSimulationData(data);
    });

    this.tagService.getRTU().on("SendRTUData", (data: any) => {
      this.handleRTU(data);
    });

    this.tagService.startRTU()
      .then(() => {
        console.log("SignalR RTU connection established");
      }).catch((error: any) => {
      console.error('SignalR RTU connection error:', error);
    });
  }

  handleSimulationData(data: any){
    // console.log(data);
    for(let tag of this.tags){
      if(tag.ioAddress == data.ioAddress.toString()) tag.value = Math.round((data.value + Number.EPSILON) * 100) / 100;
    }
  }

  handleRTU(data: any){
    // console.log(data);
    for (let tag of this.tags){
      // console.log(tag.ioAddress);
      if(tag.ioAddress == data.ioAddress.toString()) tag.value = Math.round((data.value + Number.EPSILON) * 100) / 100;
    }
  }
}
