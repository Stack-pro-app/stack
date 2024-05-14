import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/Auth/auth.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LoginRequestDto } from '../../../core/Models/login-request-dto';
import { StoreService } from '../../../core/services/store/store.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers:[AuthService]
})
export class LoginComponent implements OnInit {
  token: string = '';
  loginrequest: LoginRequestDto = {
    userName: '',
    password: '',
  };

  constructor(
    private service: AuthService,
    private builder: FormBuilder,
    private router: Router,
    private store:StoreService,
  ) {}
  loginForm!: FormGroup;
  ngOnInit(): void {
    this.loginForm = this.builder.group({
      userName: this.builder.control(
        '',
        Validators.compose([Validators.required, Validators.email])
      ),
      password: this.builder.control('', Validators.required),
    });
  }
  onLoging() {
    this.loginrequest = this.loginForm.value;
    this.service.login(this.loginrequest).subscribe({
      next: (response) => {
        this.store.setToken(response.result.token);
        console.log(response);
          console.log(response.result.token);

        this.router.navigate(['Home']);
      },
      error: (error) => {
        console.error('Login error', error);
      },
      complete: () => console.info('complete'),
    });
  }
    get Password(): FormControl {
    return this.loginForm.get('password') as FormControl;
  }
  get userName(): FormControl {
    return this.loginForm.get('userName') as FormControl;
  }
}

