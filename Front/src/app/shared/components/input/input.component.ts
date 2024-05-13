import {
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { ChatService } from '../../../core/services/Chat/chat.service';
import { SignalrService } from '../../../core/services/signalr/signalr.service';
import { FileService } from '../../../services/file.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
})
export class InputComponent implements OnInit, OnChanges {
  @ViewChild("fileUpload", {static: false}) fileUpload: ElementRef | undefined;
  @Input({ required: true }) currentChannelP: any;
  constructor(
    private builder: FormBuilder,
    private service: ChatService,
    private fileService: FileService,
    private signalrService: SignalrService
  ) {}
  public messageForm!: FormGroup;
  messageDto: any = {
    userId: 1,
    channelId: 0,
    message: '',
    parentId: 0,
  };
  files: any = [];
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
      console.log("this is the new channel : " , changes['currentChannelP'].currentValue);

    }
  }

  ngOnInit(): void {
    this.messageForm = this.builder.group({
      message: this.builder.control(''),
    });
  }

  getFileName(): any {
    return this.fileUpload?.nativeElement.files[0]?.name?? "No file selected";
  }

  onSend() {
    var signalmessageDto = {
      userId: localStorage.getItem('userId'),
      channelId: this.currentChannelP.id,
      channelString: this.currentChannelP.channelString,
      message: this.messageForm.value.message,
    };
    if (this.fileUpload) {
      const file = this.fileUpload.nativeElement.files[0];
      console.log(this.fileUpload.nativeElement.files);
      if(file){
        this.sendFile(file);
        this.fileUpload.nativeElement.value = '';
        this.messageForm.reset();
        return;
      }
    }
    this.signalrService.sendMessage(signalmessageDto);
    this.messageForm.reset();
  }
  get message(): FormControl {
    return this.messageForm.get('message') as FormControl;
  }
  //=======================================================================================================
  sendFile(file:any) {
    console.log(file);
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.error("User ID not found in local storage.");
      return;
    }
    console.log(file);
    this.fileService.uploadFile(file,this.currentChannelP.channelString,userId,this.currentChannelP.id,this.messageForm.value.message).subscribe((event: any) => {
        if (typeof (event) === 'object') {
          console.log(event.body);
        }
      });
  }
}
