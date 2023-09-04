import {Component, ViewChild} from '@angular/core';
import {Alarm, InputsDTO} from "../model/Alarm";
import {AnalogInput} from "../model/AnalogInput";
import {TagserviceService} from "../services/TagService/tagservice.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {DigitalInput} from "../model/DigitalInput";

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css']
})
export class TrendingComponent {
  Inputs: InputsDTO = {
    analogInputList: [],
    digitalInputList: []
  };
  displayedColumns: string[] = ['I/O Address', 'Name', 'Low limit', 'High limit', 'Value', 'Scan', 'Alarms'];
  displayedColumnsDig: string[] = ['I/O Address', 'Name', 'Value', 'Scan'];
  dataSource = new MatTableDataSource<AnalogInput>();
  dataSourceDig = new MatTableDataSource<DigitalInput>();
  tags: any[] = [];
  @ViewChild('paginator') paginator!: MatPaginator;
  constructor( private tagService: TagserviceService){
    this.connectHub();
  }

  ngOnInit(){
    this.tagService.getInputTags().subscribe({
      next: (val: any) => {
        console.log(val);
        this.Inputs = val;
        this.Inputs.analogInputList.forEach((x: any) => this.tags.push(x));
        this.Inputs.digitalInputList.forEach((x: any) => this.tags.push(x));
        console.log(this.tags);
        this.dataSource = new MatTableDataSource<AnalogInput>(this.Inputs.analogInputList);
        this.dataSource.paginator = this.paginator;

        this.dataSourceDig = new MatTableDataSource<DigitalInput>(this.Inputs.digitalInputList);
        this.dataSourceDig.paginator = this.paginator;
      },
      error: (err: any) => {
        console.log(err);
      }
    });

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

  isAlarmActive(alarm: Alarm, input: AnalogInput) : boolean {
    if(!input.isScanOn) return false
    if(alarm.type == 0 && input.value < alarm.threshold) return true
    if(alarm.type == 1 && input.value > alarm.threshold) return true
    return false
  }
}
