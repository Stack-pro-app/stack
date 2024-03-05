import { Component } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ChannelComponent } from '../../../shared/components/channel/channel.component';
import { Channel } from '../../../core/Models/channel';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ChannelService } from '../../../core/services/Channel/channel.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { WorkspaceService } from '../../../core/services/Workspace/workspace.service';
import { Workspace } from '../../../core/Models/workspace';

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
  id: string | null = '';
  channelRequest: any = {
    name: '',
    description: '',
    is_private: false,
    workspaceId: 0,
  };
  currentWorkspace: Workspace = {
    id: 0,
    name: '',
    MainChannel: {},
    publicChannels: [],
    privateChannels: [],
  };
  channels: any[] = [];
  currentChannel: string = '';
  constructor(
    private service: ChannelService,
    private builder: FormBuilder,
    private route: ActivatedRoute,
    private workspaceService: WorkspaceService
  ) {}
  public channelForm!: FormGroup;
  onDeletechannel() {}

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.channelForm = this.builder.group({
      channelName: this.builder.control(''),
      channelPrivate: this.builder.control(false),
      channelDescription: this.builder.control(''),
    });

    this.workspaceService
      .getWorkspace(this.id, localStorage.getItem('userId'))
      .subscribe({
        next: (response) => {
          console.log(response);
          this.currentWorkspace = response.result;
          this.channels = this.currentWorkspace.privateChannels;
          console.log(this.channels);
        },
        error: (error) => {
          console.error('Login error', error);
        },
        complete: () => console.info('complete'),
      });
  }
  reload() {
    this.workspaceService
      .getWorkspace(this.id, localStorage.getItem('userId'))
      .subscribe({
        next: (response) => {
          console.log(response);
          this.currentWorkspace = response.result;
          this.channels = this.currentWorkspace.privateChannels;
          for (let chanel of this.currentWorkspace.publicChannels) {
            this.channels.push(chanel);
          }
          console.log(this.channels);
        },
        error: (error) => {
          console.error('Login error', error);
        },
        complete: () => console.info('complete'),
      });
  }
  onCreateChannel() {
    this.channelRequest.name = this.channelForm.value.channelName;
    this.channelRequest.workspaceId = this.currentWorkspace.id;
    this.channelRequest.is_private = this.channelForm.value.channelPrivate;
    this.channelRequest.description = this.channelForm.value.channelDescription;
    this.service.CreateChannel(this.channelRequest).subscribe({
      next: (response) => {
        console.log(response);
        this.reload();
      },
      error: (error) => {
        console.error('Login error', error);
      },
      complete: () => console.info('complete'),
    });
  }
}
