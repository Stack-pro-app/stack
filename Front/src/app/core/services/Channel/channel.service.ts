import { Injectable } from '@angular/core';
import { Channel } from '../../Models/channel';

@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  chanels: Channel[] = [
    {
      id: 1,
      name: 'Channel 1',
      Description: '',
      IsPriate: false,
      WorkspaceId: 0,
    },
    {
      id: 1,
      name: 'Channel 1',
      Description: '',
      IsPriate: false,
      WorkspaceId: 0,
    },
    {
      id: 1,
      name: 'Channel 1',
      Description: '',
      IsPriate: false,
      WorkspaceId: 0,
    },
    {
      id: 1,
      name: 'Channel 1',
      Description: '',
      IsPriate: false,
      WorkspaceId: 0,
    },
    {
      id: 1,
      name: 'Channel 1',
      Description: '',
      IsPriate: false,
      WorkspaceId: 0,
    },
    {
      id: 1,
      name: 'Channel 1',
      Description: '',
      IsPriate: false,
      WorkspaceId: 0,
    },
  ];

  constructor() {}

  CreateChannel(channelName: string, channelPrivate: boolean) {
    this.chanels.push({
      id: 1,
      name: channelName,
      Description:'',
      IsPriate:false,
      WorkspaceId:0
    });
  }
}
