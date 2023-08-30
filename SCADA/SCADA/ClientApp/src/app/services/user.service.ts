import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginDTO, User} from "../login/login.component";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  login(loginDTO : LoginDTO) :Observable<User> {
    return this.http.post<User>(`${this.baseUrl}user/login`, loginDTO);
  }
  register(user : User) :Observable<any> {
    return this.http.post<any>(`${this.baseUrl}user`, user);
  }
}
