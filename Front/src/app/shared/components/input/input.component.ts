import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
})
export class InputComponent implements OnInit {
  constructor(private builder: FormBuilder) {}
  public messageForm!: FormGroup;
  messageDto:any={
      message: '',
  }

  ngOnInit(): void {
    this.messageForm = this.builder.group({
      message: this.builder.control(''),
    });
  }

  onSend() {
    this.messageDto.message = this.messageForm.value;
    console.log(this.message);
  }
  get message(): FormControl {
    return this.messageForm.get('message') as FormControl;
  }
}
