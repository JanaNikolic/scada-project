import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    let body = {id: 0, email: "email", password: "password", role: "USER"};
    // let body = {email: "email", password: "pass"};
    http.post<any>(baseUrl + 'user', body).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }

}
