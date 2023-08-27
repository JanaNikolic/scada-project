import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {

  registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(3)]),
  });

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string
               , private snackBar: MatSnackBar
  ) {
  }

  registerUser() {
    if (this.registerForm.valid) {
      const { email, password } = this.registerForm.value;
      const body = { email, password, Role: "USER" };
      this.http.post<any>(`${this.baseUrl}user`, body).subscribe(
        (result: any) => {
          console.log(result);
          this.registerForm.reset();
          this.snackBar.open('Added user successfully!', "", {duration: 2000});
        },
        (error: any) => {
          console.error(error);
          this.snackBar.open('Something went wrong, try again!', "", {duration: 2000});
        }
      );
    }
  }
}
