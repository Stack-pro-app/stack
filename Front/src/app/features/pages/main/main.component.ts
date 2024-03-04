import { Component } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ChannelComponent } from '../../../shared/components/channel/channel.component';
import { Channel } from '../../../core/Models/channel';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ChannelService } from '../../../core/services/Channel/channel.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    InputComponent,
    RouterLink,
    ChannelComponent,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent {
  channels: Channel[];
  currentChannel: string;
  constructor(private service: ChannelService, private builder: FormBuilder) {
    this.channels = service.chanels;
    this.currentChannel = this.channels[0].name;
  }
  public channelForm!: FormGroup;
  onDeletechannel() {}

  ngOnInit() {
    this.channelForm = this.builder.group({
      channelName: this.builder.control(''),
      channelPrivate: this.builder.control(false),
    });
  }

  onCreateChannel() {
    let channel = this.channelForm.value;
    if (channel.channelName === '') {
      return;
    }
    this.service.CreateChannel(channel.channelName, channel.channelPrivate);
  }
}
