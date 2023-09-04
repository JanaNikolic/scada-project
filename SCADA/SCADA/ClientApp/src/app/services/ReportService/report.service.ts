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

  getAlarmsByRange(startDate: any, endDate: any): Observable<ActivatedAlarm[]> {
    return this.http.get<ActivatedAlarm[]>(
      `${this.baseUrl}report/alarms-in-range?startDate=${startDate}&endDate=${endDate}`
    );

  }

  getRecordsByRange(start: any, end: any): Observable<TagRecord[]> {
    return this.http.get<TagRecord[]>(`${this.baseUrl}report/records-in-range?startDate=${start}&endDate=${end}`);
  }

  getAllcurrentInputValues(): Observable<InputList> {
    return this.http.get<InputList>(this.baseUrl + "report/ai/");
  }

  getAllTagValues(id: number): Observable<TagRecord[]> {
    return this.http.get<TagRecord[]>(this.baseUrl + "report/tag/" + id);
  }
}
