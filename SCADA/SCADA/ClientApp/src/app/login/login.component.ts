import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

export interface User {
  Email: string;
  Password: string;
  Role: string;
}

export interface LoginDTO {
  Email: string;
  Password: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private userService: UserService,
    private snackBar: MatSnackBar,
  ) {}

  public login() {
    if (
      this.loginForm.value.email?.trim() != '' &&
      this.loginForm.value.password?.trim() != ''
    ) {
      const values = this.loginForm.value;
      const loginDTO: LoginDTO = {
        Password: values.password!,
        Email: values.email!,
      };

      this.userService.login(loginDTO).subscribe({
        next: (value: User) => {
          console.log(value);
          if (value.Role === "USER") this.router.navigate(['/trending']);
          else this.router.navigate(['/home']);
        },
        error: (error: any) => {
          console.log(error);
          this.snackBar.open('Something went wrong, try again!', '', {
            duration: 2000,
          });
        },
      });
    }
  }
}
