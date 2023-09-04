import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { InputList } from 'src/app/model/InputList';
import { OutputList } from 'src/app/model/OutputList';
import { TagDTO } from 'src/app/model/TagDTO';
import { ValueDTO } from 'src/app/model/valueDTO';
import {HttpTransportType, HubConnection, HubConnectionBuilder} from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class TagserviceService {

  private hubConnection: HubConnection;
  private rtuConnection: HubConnection;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:7026/hubs/simulation", {skipNegotiation:true, transport: HttpTransportType.WebSockets})
      .withAutomaticReconnect()
      .build();

    this.rtuConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:7026/hubs/rtu", {skipNegotiation:true, transport: HttpTransportType.WebSockets})
      .withAutomaticReconnect()
      .build();

  }

  startConnection() {
    return this.hubConnection.start();
  }

  startRTU(){
    return this.rtuConnection.start()
      .catch(error => {
        console.error("Error starting SignalR RTU connection:", error);
      });
  }

  stopConnection() {
    return this.hubConnection.stop();
  }

  stopRTU(){
    return this.rtuConnection.stop();
  }

  getHubConnection() {
    return this.hubConnection;
  }

  getRTU(){
    return this.rtuConnection;
  }

  getInputTags(): Observable<InputList> {
    return this.http.get<InputList>(this.baseUrl + "tag/input");
  }

  getOutputTags(): Observable<OutputList> {
    return this.http.get<OutputList>(this.baseUrl + "tag/output");
  }

  deleteTag(id: number): Observable<any> {
    return this.http.delete<any>(this.baseUrl + "tag/"+ id);
  }

  toggleScan(id: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + "tag/"+ id);
  }

  addInputTag(tag: TagDTO): Observable<any> {
    return this.http.post<any>(this.baseUrl + "tag", tag);
  }

  addOutputValue(valueDTO: ValueDTO): Observable<any> {
    return this.http.post<any>(this.baseUrl + "tag/add-output", valueDTO);
  }

  addAlarm(body: any) : Observable<any> {
    return this.http.post<any>(this.baseUrl + "alarm", body);
  }

  deleteAlarm(id: number) : Observable<any> {
    return this.http.delete<any>(this.baseUrl + "alarm/" + id);
  }
}
