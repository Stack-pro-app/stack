import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../core/services/Auth/auth.service';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  errorMessages: { [key: string]: string } = {};
  registerRequest: any = {
    email: '',
    name: '',
    password: '',
    rolename:''
  };
  constructor(
    private service: AuthService,
    private router: Router,
    private builder: FormBuilder
  ) {}

  public registerForm!: FormGroup;

  ngOnInit() {
    this.registerForm = this.builder.group({
      name: this.builder.control('', Validators.required),
      email: this.builder.control(
        '',
        Validators.compose([Validators.required, Validators.email])
      ),
      password: this.builder.control(
        '',
        Validators.compose([Validators.required, Validators.minLength(8)])
      ),
      confirm_password: this.builder.control(
        '',
        Validators.compose([Validators.required, Validators.minLength(8)])
      ),
    });
  }

  onRegister() {
    console.log(this.registerForm.value);
    if (
      this.registerForm.get('password')?.value?.trim() !==
        this.registerForm.get('confirm_password')?.value?.trim() &&
      this.registerForm.get('password')?.value?.length > 0 &&
      this.registerForm.get('confirm_password')?.value?.length > 0
    ) {
      this.errorMessages['equal'] = 'Passwords do not match';
    }

    if (this.registerForm.valid) {
      this.registerRequest = {
        name : this.registerForm.value.name,
        email : this.registerForm.value.email,
        password : this.registerForm.value.password,
      };
      this.service.register(this.registerRequest).subscribe({
        next: (response) => {
          this.router.navigate(['/Home'], {
            queryParams: { registration: true },
          });
        },
      error:  (error) => {
          console.log(error);
        },
      complete: () => console.info('complete'),
      }

      );
    } else console.log('failed a broski');
  }


  get Name(): FormControl {
    return this.registerForm.get('name') as FormControl;
  }
  // get PhoneNumber(): FormControl {
  //   return this.registerForm.get('phoneNumber') as FormControl;
  // }
  get Email(): FormControl {
    return this.registerForm.get('email') as FormControl;
  }
  get Password(): FormControl {
    return this.registerForm.get('password') as FormControl;
  }
  get Confirm_Password(): FormControl {
    return this.registerForm.get('confirm_password') as FormControl;
  }
  get RoleName(): FormControl {
    return this.registerForm.get('rolename') as FormControl;
  }
}

