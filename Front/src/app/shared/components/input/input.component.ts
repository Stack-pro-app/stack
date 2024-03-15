import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { ChatService } from '../../../core/services/Chat/chat.service';
import { SignalrService } from '../../../core/services/signalr/signalr.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
})
export class InputComponent implements OnInit, OnChanges {
  @Input({ required: true }) currentChannelP: any;
  @Output() uploadFile = new EventEmitter<boolean>();
  constructor(private builder: FormBuilder, private service: ChatService,private signalrService : SignalrService) {}
  public messageForm!: FormGroup;
  messageDto: any = {
    userId: 1,
    channelId: 0,
    message: '',
    parentId: 0,
  };
  selectedFilePreview: string | ArrayBuffer | null| undefined = null; // Variable to hold file preview URL

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
    }
  }

  ngOnInit(): void {
    this.messageForm = this.builder.group({
      message: this.builder.control(''),
      attachment: this.builder.control(null), // Add attachment control

    });
   
    
  }
  onClick():void{
        this.uploadFile.emit(true);
  }
  onSend() {
    this.messageDto = {
      userId: localStorage.getItem('userId'),
      channelId: this.currentChannelP.id,
      message: this.messageForm.value.message,
      parentId: null,
    };
     const signalmessageDto = {
       userId: localStorage.getItem('userId'),
       channelId: this.currentChannelP.id,
       ChannelString: this.currentChannelP.channelString,
       message: this.messageForm.value.message,
       parentId: null,
     };
    this.signalrService.sendMessage(signalmessageDto);
    this.service.SendMessage(this.messageDto).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Updating  error', error);
      },
      complete: () => {
        console.info('completed');
      },
    });
  }
  get message(): FormControl {
    return this.messageForm.get('message') as FormControl;
  }
  
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    this.messageForm.patchValue({
      attachment: file
    });
    if (file.type.startsWith('image')) {
      const reader = new FileReader();
      reader.onload = (e) => {
        this.selectedFilePreview = e.target?.result;
      };
      reader.readAsDataURL(file);
    } else if (file.type === 'application/pdf') {
      const reader = new FileReader();
      reader.onload = (e) => {
        this.selectedFilePreview = e.target?.result;
      };
      reader.readAsDataURL(file);
    } else {
      // For non-image and non-PDF files, show download link
      this.selectedFilePreview = null;
    }
    // You can now handle the selected file, for example, display its preview.
    // For simplicity, let's just log the file details to console.
    console.log('Selected File:', file);
  }
  
}
