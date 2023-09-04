import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedAlarm } from 'src/app/model/ActivatedAlarm';
import { InputList } from 'src/app/model/InputList';
import { TagRecord } from 'src/app/model/TagRecord';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAlarmsByPriority(priority: string): Observable<ActivatedAlarm[]> {
    return this.http.get<ActivatedAlarm[]>(this.baseUrl + "report/alarms/" + priority);
  }

  getAlarmsByRange(start: string, end: string): Observable<ActivatedAlarm[]> {
    let params = new HttpParams();

    // Add 'start' and 'end' parameters to the HttpParams
    params = params.set('start', start); // Convert Date to ISO string
    params = params.set('end', end); // Convert Date to ISO string

    return this.http.get<ActivatedAlarm[]>(this.baseUrl + "report/alarms-in-range/", {params});
  }

  getRecordsByRange(start: Date, end: Date): Observable<ActivatedAlarm[]> {
    let params = new HttpParams();

    // Add 'start' and 'end' parameters to the HttpParams
    params = params.set('start', start.toISOString()); // Convert Date to ISO string
    params = params.set('end', end.toISOString()); // Convert Date to ISO string

    return this.http.get<ActivatedAlarm[]>(this.baseUrl + "report/records-in-range/", {params});
  }

  getAllcurrentInputValues(): Observable<InputList> {
    return this.http.get<InputList>(this.baseUrl + "report/ai/");
  }

  getAllTagValues(id: number): Observable<TagRecord[]> {
    return this.http.get<TagRecord[]>(this.baseUrl + "report/" + id);
  }
}
