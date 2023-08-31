import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { InputList } from 'src/app/model/InputList';
import { OutputList } from 'src/app/model/OutputList';
import { TagDTO } from 'src/app/model/TagDTO';
import { ValueDTO } from 'src/app/model/valueDTO';

@Injectable({
  providedIn: 'root'
})
export class TagserviceService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

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

}
