import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ChannelComponent } from '../../../shared/components/channel/channel.component';
import { Channel } from '../../../core/Models/channel';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ChannelService } from '../../../core/services/Channel/channel.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { WorkspaceService } from '../../../core/services/Workspace/workspace.service';
import { Workspace } from '../../../core/Models/workspace';
import { SignalrService } from '../../../core/services/signalr/signalr.service';
import { UserService } from '../../../core/services/user.service';

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
export class MainComponent implements OnInit, OnChanges {
  searchTerm: string = '';
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
    channelString: '',
    created_at: {},
    description: '',
    id: 0,
    is_private: false,
    name: '',
  };
  users: any[] = [
    {
      img: 'https://i.ibb.co/XJ5y9WM/me.jpg',

      name: 'Reda Mountassir',
    },
    {
      img: null,
      name: 'enma No Katana',
    },
  ];
  CUsers!: any[];
  channels: Channel[] = [];
  constructor(
    private signalrService: SignalrService,
    private userService: UserService,
    private service: ChannelService,
    private builder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private workspaceService: WorkspaceService
  ) {}
  public channelForm!: FormGroup;
  public workspaceForm!: FormGroup;
  public userForm!: FormGroup;
  receivedMessage: any;
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
      const messageDto = {
        userId: localStorage.getItem('userId'),
        channelId: 14,
        ChannelString: '1D96A361-E812-460E-A21D-429B0C62F935',
        message: 'this.messageForm.value.message',
      };
      this.signalrService.sendMessage(messageDto);
    }
  }
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
    this.userForm = this.builder.group({
      userEmail: this.builder.control(
        '',
        Validators.compose([Validators.required, Validators.email])
      ),
    });

    this.workspaceService
      .getWorkspace(this.id, localStorage.getItem('userId'))
      .subscribe({
        next: (response) => {
          this.currentWorkspace = response.result;
          console.log('Wos', this.currentWorkspace);

          this.channels = this.currentWorkspace.privateChannels;
 for (let chanel of this.currentWorkspace.publicChannels) {
   this.channels.push(chanel);
 }
          this.currentChannelP = {
            channelString: response.result.mainChannel.channelString,
            created_at: response.result.mainChannel.created_at,
            description: response.result.mainChannel.description,
            id: response.result.mainChannel.id,
            is_private: response.result.mainChannel.is_private,
            name: response.result.mainChannel.name,
          };
          console.log(this.currentChannelP);
          console.log(this.channels);
          this.onGetUsers();
        },
        error: (error) => {
          console.error('Login error', error);
        },
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
        },
        error: (error) => {
          console.error('Reload error', error);
        },
      });
    this.onGetUsers();
  }
  onCreateChannel() {
    this.channelRequest.name = this.channelForm.value.channelName;
    this.channelRequest.workspaceId = this.currentWorkspace.id;
    this.channelRequest.is_private = this.channelForm.value.channelPrivate;
    this.channelRequest.description = this.channelForm.value.channelDescription;
    this.service.CreateChannel(this.channelRequest).subscribe({
      next: (response) => {
        this.reload();
      },
      error: (error) => {
        console.error('Login error', error);
      },
      complete: () => console.info('complete'),
    });
  }
  onDeleteWorkspace() {
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
        this.reload();
      },
    });
  }
  onChangeChannel(channel: Channel) {
    this.currentChannelP = channel;
    console.log('HEEEEEEEEEEEREE', this.currentChannelP);
  }
  onDeleteChannel() {
    this.service.Delete(this.currentChannelP.id).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Updating  error', error);
      },
      complete: () => {
        this.reload();
      },
    });
  }
  onUpdateChannel() {
    const data = {
      id: this.currentChannelP.id,
      name: this.channelForm.value.channelName,
      description: this.currentChannelP.description,
      is_private: this.channelForm.value.channelPrivate,
    };
    this.service.Update(data).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Updating  error', error);
      },
      complete: () => {
        this.reload();
      },
    });
  }
  onAddUser() {
    let userId = 0;
    this.userService.FindUserByEmail(this.userForm.value.userEmail).subscribe({
      next: (response) => {
        console.log(response.result.id);
        userId = response.result.id;
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {
        this.userService
          .addUserToWorkSpace(userId, this.currentWorkspace.id)
          .subscribe({
            next: (response) => {},
            error: (error) => {
              console.error('get Users  error', error);
            },
            complete: () => {
              this.reload();
            },
          });
      },
    });
  }
  onRemoveUser(data: any) {
    //Todo Remove User
  }
  onGetUsers() {
    this.userService.getUersFromWorkSpace(this.currentWorkspace.id).subscribe({
      next: (response) => {
        this.CUsers = response;
        console.log('Users', this.CUsers);
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {},
    });
  }
  onDeleteUserFromWs(id: any) {
    console.log('deleted user : ', id);
  }
  filterItems() {}
}
