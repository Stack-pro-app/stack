import { Component } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ChannelComponent } from '../../../shared/components/channel/channel.component';
import { Channel } from '../../../core/Models/channel';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ChannelService } from '../../../core/services/Channel/channel.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
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
  currentChannelP: Channel = {
    channelString:'',
    created_at: {},
    description: '',
    id: 0,
    is_private: false,
    name: '',
  };
  channels: any[] = [];
  currentChannel: string = '';
  constructor(
    private service: ChannelService,
    private builder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private workspaceService: WorkspaceService
  ) {}
  public channelForm!: FormGroup;
  public workspaceForm!: FormGroup;
  onDeletechannel() {}

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.channelForm = this.builder.group({
      channelName: this.builder.control(''),
      channelPrivate: this.builder.control(false),
      channelDescription: this.builder.control(''),
    });
    this.workspaceForm = this.builder.group({
      workspaceName: this.builder.control(''),
    });

    this.workspaceService
      .getWorkspace(this.id, localStorage.getItem('userId'))
      .subscribe({
        next: (response) => {
          console.log(response);
          this.currentWorkspace = response.result;
          this.channels = this.currentWorkspace.privateChannels;

          this.currentChannelP = {
            channelString: response.result.mainChannel.channelString,
            created_at: response.result.mainChannel.created_at,
            description: response.result.mainChannel.description,
            id: response.result.mainChannel.id,
            is_private: response.result.mainChannel.is_private,
            name: response.result.mainChannel.name,
          };
          console.log('Current Channel', this.currentChannelP);
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
  onDeleteChannel() {
    this.workspaceService.Delete(this.currentWorkspace.id).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigate(['/Home']);
      },
      error: (error) => {
        console.error('Login error', error);
      },
      complete: () => console.info('complete'),
    });
  }
  onUpdateWorkspace() {
    const name = this.workspaceForm.value.workspaceName;
    console.log(name);
    this.workspaceService.Update(this.currentWorkspace.id, name).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Updating  error', error);
      },
      complete: () => {
        console.info('completed');
        this.reload();
      },
    });
  }
  onChangeChannel(channel:Channel){
  this.currentChannelP = channel;
  }
}
