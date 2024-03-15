import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { WorkspaceService } from '../../../core/services/Workspace/workspace.service';
import { Workspace } from '../../../core/Models/workspace';

@Component({
  selector: 'app-create-work-space',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './create-work-space.component.html',
  styleUrl: './create-work-space.component.css',
})
export class CreateWorkSpaceComponent implements OnInit {
  inputText: string = '';
  displayedText: string = '';
  WorkspaceName: string = '';
  FirstChannelName: string = '';
  step: number = 1;
  WorkspaceRequest: any = {
    id: 0,
    name: '',
    MainChannel: '',
    userIds: [],
    channelIds: [],
    emails: [],
  };
  selectedEmailDomain: string = '';
  userIds :number[] = [];

  emailDomains: string[] = ['gmail.com', 'example.com'];
  constructor(
    private builder: FormBuilder,
    private router: Router,
    private service: WorkspaceService
  ) {}
  form!: FormGroup;
  ngOnInit(): void {
    this.form = this.builder.group({
      WName: this.builder.control(
        '',
        Validators.compose([Validators.required, Validators.maxLength(50)])
      ),
      CName: this.builder.control(''),
    });
  }

  updateWorksSpaceName() {
    this.WorkspaceName = this.inputText;
  }
  updateFirstChannel() {
    this.FirstChannelName = this.displayedText;
  }

  onNextClick() {
    if (this.step === 1) {
      // You can perform validation, etc., based on your requirements
      if (this.CNmae.invalid || this.WNmae.invalid) {
        // Handle validation error
        console.log('err');
        return;
      }
      console.log(this.step);
      this.step = 2; // Move to the next step
    } else if (this.step === 2) {
      // Process the form data, you can access it using this.form.value
      /* */
      this.WorkspaceRequest.name = this.form.value.WName;
      this.WorkspaceRequest.MainChannel = this.form.value.CName;

      // Reset the form or navigate to the next component/page as needed
      this.step = 3;
    } else if (this.step === 3) {
      this.form.get('coworkerEmails')?.value.split(',');
      console.log('Emails', this.form.value.coworkerEmails);


    }
  }

  onEmailInput(event: Event): void {
    const textareaValue = (event.target as HTMLTextAreaElement).value;
    const cursorPosition = (event.target as HTMLTextAreaElement).selectionEnd;

    // Check if the cursor is inside an email address
    const regex = /\S+@\S+/;
    const matches = regex.exec(textareaValue);
    if (
      matches &&
      cursorPosition >= matches.index &&
      cursorPosition <= matches.index + matches[0].length
    ) {
      // Get the domain of the email and show it as an option
      const email = matches[0];
      const domain = email.split('@')[1];
      this.selectedEmailDomain = domain;
    } else {
      this.selectedEmailDomain = '';
    }
  }
  selectEmailDomain(domain: string): void {
    // Get the current value of the textarea
    const currentTextareaValue = this.form.get('coworkerEmails')?.value;

    if (!currentTextareaValue) {
      console.error('Textarea value is undefined or null.');
      return;
    }

    // Get the cursor position within the textarea
    const textareaElement = document.activeElement as HTMLTextAreaElement;
    const cursorPosition = textareaElement.selectionStart;

    // Check if cursor position is within bounds
    if (cursorPosition === undefined || cursorPosition < 0 || cursorPosition > currentTextareaValue.length) {
      console.error('Cursor position is out of bounds.');
      return;
    }

    // Construct the updated value with the selected email domain inserted
    const updatedValue =
      currentTextareaValue.substring(0, cursorPosition) +
      domain +
      currentTextareaValue.substring(cursorPosition);

    // Update the value of the textarea in the form
    this.form.get('coworkerEmails')?.setValue(updatedValue);

    // Set the cursor position after the inserted email domain
    const newCursorPosition = cursorPosition + domain.length;
    textareaElement.setSelectionRange(newCursorPosition, newCursorPosition);
  }


  extractEmail(text: string, cursorPosition: number): string {
    // Check if the text is defined
    if (!text) {
      return '';
    }

    // Search for the beginning and end of the email address based on cursor position
    let startIndex = cursorPosition;
    let endIndex = cursorPosition;

    // Move the start index backward until the beginning of the email or whitespace is found
    while (startIndex > 0 && /\S/.test(text[startIndex - 1])) {
      startIndex--;
    }

    // Move the end index forward until the end of the email or whitespace is found
    while (endIndex < text.length && /\S/.test(text[endIndex])) {
      endIndex++;
    }

    // Extract the email address substring
    const email = text.substring(startIndex, endIndex);

    return email;
  }




  get CNmae(): FormControl {
    return this.form.get('CName') as FormControl;
  }
  get WNmae(): FormControl {
    return this.form.get('CName') as FormControl;
  }
  get Emails(): FormControl {
    return this.form.get('coworkersEmails') as FormControl;
  }
}
