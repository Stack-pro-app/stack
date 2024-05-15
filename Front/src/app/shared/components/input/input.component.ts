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
  fileLoading : boolean = false;
  imageDataUrl: string | ArrayBuffer | null = null;
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
    if (!this.fileUpload) {
      return false;
    }
    if(this.fileUpload.nativeElement.files.length == 0){
      return false;
    }
    return this.fileUpload?.nativeElement.files[0]?.name?? "Unknown";
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
      if(file){
        this.sendFile(file);
        this.fileUpload.nativeElement.value = '';
        this.messageForm.reset();
        this.imageDataUrl = null;
        this.fileService.fileSent = true;
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
  onFileChange(event: any) {
    this.fileLoading = true
    if(event.target.files.length > 0) {
      this.fileLoading = false;
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (_event) => {
        this.imageDataUrl = reader.result;
      };
    }
    else{
      setTimeout(()=>{
        this.fileLoading = false;
      },5000);
    }
  }

  GetFile(){
    console.log(this.fileUpload?.nativeElement.files[0]);
    return this.fileUpload?.nativeElement.files[0];
  }

  sendFile(file:any) {
    console.log(file);
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.error("User ID not found in local storage.");
      return;
    };
    console.log(file.name)
    this.fileService.uploadFile(file,this.currentChannelP.channelString,userId,this.currentChannelP.id,this.messageForm.value.message).subscribe(
      {
        complete: () => {
        },
      }
      );
  }
  isImageFileName(fileName: string | null): boolean {
    if (!fileName) {
      return false;
    }
    const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif', '.bmp'];
    const lowerCaseFileName = fileName.toLowerCase();
    return imageExtensions.some(ext => lowerCaseFileName.endsWith(ext));
  }

  getFileSent(){
    return this.fileService.fileSent;
  }

  getFileExtension(fileName: string): string {
    return fileName.split('.').pop()!.toLowerCase();
  }

  getFileImg(): string {
    const extention = this.getFileExtension(this.fileUpload?.nativeElement.files[0]?.name);
    const availableExt = ['pdf','docx','xlsx','pptx','txt','csv'];
    console.log(extention);
    if(availableExt.includes(extention)){
      return "../../../../assets/img/"+extention+".svg";
    }
    return "../../../../assets/img/unknown.svg";
  }
}
