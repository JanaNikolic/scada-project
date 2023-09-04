import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '../services/user.service';
import { User } from '../login/login.component';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css'],
})
export class AddUserComponent {
  registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    userType: new FormControl('USER', [Validators.required]),
  });

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private snackBar: MatSnackBar,
    private userService: UserService
  ) {}

  registerUser() {
    if (this.registerForm.valid) {
      const { email, password, userType } = this.registerForm.value;
      const body: User = { Email: email!, Password: password!, Role: userType !== null && userType !== undefined ? userType : 'USER' };
      this.userService.register(body).subscribe({
        next: (result: any) => {
          console.log(result);
          this.registerForm.reset();
          this.registerForm.get('userType')?.setValue('USER');
          this.snackBar.open('Added user successfully!', '', {
            duration: 2000,
          });
        },
        error: (error: any) => {
          console.error(error);
          this.snackBar.open('Something went wrong, try again!', '', {
            duration: 2000,
          });
        },
      });
    }
  }
}
