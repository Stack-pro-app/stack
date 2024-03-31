import {
  Component,
  Input,
  OnChanges,
  OnInit,
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

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
})
export class InputComponent implements OnInit, OnChanges {
  @Input({ required: true }) currentChannelP: any;
  constructor(
    private builder: FormBuilder,
    private service: ChatService,
    private signalrService: SignalrService
  ) {}
  public messageForm!: FormGroup;
  messageDto: any = {
    userId: 1,
    channelId: 0,
    message: '',
    parentId: 0,
  };
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
    }
  }

  ngOnInit(): void {
    this.messageForm = this.builder.group({
      message: this.builder.control(''),
    });
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
    this.messageForm.reset();
  }
  get message(): FormControl {
    return this.messageForm.get('message') as FormControl;
  }
}
