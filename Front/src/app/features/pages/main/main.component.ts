import { Component, ElementRef, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ChannelComponent } from '../../../shared/components/channel/channel.component';
import { Channel } from '../../../core/Models/channel';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
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
import { StoreService } from '../../../core/services/store/store.service';
import { ThemeSwitcherComponent } from '../../../shared/components/theme-switcher/theme-switcher.component';
import { NotificationComponent } from '../notification/notification.component';
import { Profile2Component } from '../profile-2/profile-2.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    InputComponent,
    RouterLink,
    ChannelComponent,
    ReactiveFormsModule,
    CommonModule,
    ThemeSwitcherComponent,
    FormsModule,
    NotificationComponent,
    Profile2Component,
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit, OnChanges {
  Loading: Boolean = false;
  @ViewChild('inputModify') inputModify: ElementRef | undefined;
  foundUser:any = {
    name:"Couldn't find User",
    email:"Couldn't find user",
  };
  showDeleteButton:Boolean = false;
  channelToDelete:Channel|null = null;
  channelToModify:Channel|null = null;
  channelToModifyName:string = '';
  channelUsers: any[] = [];
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
    channels: [],
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
  isAdmin: Boolean = false;
  CUsers!: any[];
  channels: Channel[] = [];
  constructor(
    private signalrService: SignalrService,
    private userService: UserService,
    private channelService:ChannelService,
    private service: ChannelService,
    private builder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private workspaceService: WorkspaceService,
    private store: StoreService,
  ) {}
  public channelForm!: FormGroup;
  public workspaceForm!: FormGroup;
  public userForm!: FormGroup;
  receivedMessage: any;
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentChannelP'] && changes['currentChannelP'].currentValue) {
      //add smt ?
    }
  }
  ngOnInit() {
    this.isAdmin = this.store.isAdmin();
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

          this.channels = this.currentWorkspace.channels;

          this.currentChannelP = {
            channelString: response.result.mainChannel.channelString,
            created_at: response.result.mainChannel.created_at,
            description: response.result.mainChannel.description,
            id: response.result.mainChannel.id,
            is_private: response.result.mainChannel.is_private,
            name: response.result.mainChannel.name,
          };
          this.channels.push(this.currentChannelP);


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
          this.channels = this.currentWorkspace.channels;
          this.channels.push(response.result.mainChannel);
          this.currentChannelP = {
            channelString: response.result.mainChannel.channelString,
            created_at: response.result.mainChannel.created_at,
            description: response.result.mainChannel.description,
            id: response.result.mainChannel.id,
            is_private: response.result.mainChannel.is_private,
            name: response.result.mainChannel.name,
          };
        },
        error: (error) => {
          console.error('Reload error', error);
        },
      });
    this.onGetUsers();
  }

  isSelf(id:number){
    return id.toString() == localStorage.getItem('userId');
  }

  get filteredUsers() {
    if (!this.searchTerm) {
      return this.CUsers;
    }
    return this.CUsers.filter(user =>
      user.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }


  fetchMembers() {
    this.userService.getChannelUsers(this.currentChannelP.id).subscribe({
      next: (response) => {
        console.log(response.result);
        this.channelUsers = response.result;
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {},
    });
  }
  AddUserToChannel(userId: number){
    this.channelService.AddUserToChannel(this.currentChannelP.id,userId).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {
        this.fetchMembers();
      },
    });
  }
  isUserInChannel(userId:number){
    return this.channelUsers.some(user=>user.id == userId);
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
        console.log(this.channelRequest);

        console.error('Chaneel creating error', error);
      },
      complete: () =>{

      },
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
    this.workspaceService.Update(this.currentWorkspace.id, name).subscribe({
      next: (response) => {
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
    this.service.GetChannelById(channel.id).subscribe({
      next: (response) => {
        this.currentChannelP=response.result;
      },
      error: (error) => {
        console.error('Getting Channel  error', error);
      },
      complete: () => {
        console.log('Getting Channel  completed');
      }
  });
  }
  onDeleteChannel() {
    if(this.channelToDelete){
    this.service.Delete(this.channelToDelete.id).subscribe({
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
  }
  onUpdateChannel() {
    const data = {
      id: this.channelToModify?.id,
      name: this.channelToModifyName
    };
    console.log(data);
    this.service.Update(data).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Updating  error', error);
      },
      complete: () => {
        this.channelToModify = null;
        this.channelToModifyName = '';
        this.reload();
      },
    });
  }
  onFindUser() {
    let userId = 0;
    this.userService.FindUserByEmail(this.userForm.value.userEmail).subscribe({
      next: (response) => {
        userId = response.result.id;
        this.foundUser = response.result;
        console.log(this.foundUser);
        this.Loading = !this.Loading;
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {
       /* this.userService
          .addUserToWorkSpace(userId, this.currentWorkspace.id)
          .subscribe({
            next: (response) => {},
            error: (error) => {
              console.error('get Users  error', error);
            },
            complete: () => {
              this.reload();
            },
          });*/
      },
    });
  }
  onRemoveUser(data: any) {
    //Todo Remove User
  }
  isMain(channel:Channel){
    return channel.name == "main";
  }
  modifyChannel(channel:Channel){
    this.channelToModify = channel;
    this.channelToModifyName = channel.name;
    setTimeout(() => {
      this.inputModify?.nativeElement.focus();
      const clickEvent = new MouseEvent('click');
      this.inputModify?.nativeElement.dispatchEvent(clickEvent);
    });
  }
  isModify(channel:Channel){
    return this.channelToModify == channel;
  }
  cancelModify(event: MouseEvent){
    if(this.inputModify){
      const target = event.target as HTMLElement;
      if (!this.inputModify?.nativeElement.contains(target)) {
        this.channelToModify = null;
        this.channelToModifyName = '';
      }
    }
  }
  onGetUsers() {
    this.userService.getUersFromWorkSpace(this.currentWorkspace.id).subscribe({
      next: (response) => {
        this.CUsers = response.result;
        this.CUsers = this.CUsers
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {},
    });
  }
  setChannelToDelete(channel: any) {
    this.channelToDelete = channel;
  }
  onDeleteUserFromWs(id: any) {
    this.userService
      .deleteUserFromWorkSpace(id, this.currentWorkspace.id)
      .subscribe({
        next: (response) => {
          console.log('Confirmation', response);
        },
        error: (error) => {
          console.error('get Users  error', error);
        },
        complete: () => {
          this.reload();
        },
      });
  }
  filterItems() {}
  handleButtonClick(event: Event) {
    event.stopPropagation();
}
  onInviteUser() {
    this.workspaceService
      .onInviteUser(this.foundUser.id, this.currentWorkspace.id)
      .subscribe({
        next: (response) => {
          console.log('Confirmation', response);
        },
        error: (error) => {
          console.error('get Users  error', error);
        },
        complete: () => {
          this.reload();
        },
      });
  }
  OneToOne(userId:any) {
    const requset = {
      "user1": localStorage.getItem('userId'),
      "user2": userId,
      "workspaceId": this.currentWorkspace.id
    }
    console.log(requset);
    this.service.OneToOne(requset).subscribe({
      next: (response) => {
        console.log('Confirmation', response);
        this.currentChannelP =response.result;
      },
      error: (error) => {
        console.error('get Users  error', error);
      },
      complete: () => {
      },
    });
  }
}
