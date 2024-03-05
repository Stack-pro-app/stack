import { Injectable } from '@angular/core';
import { Channel } from '../../Models/channel';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  url: string = 'http://localhost:5149/api/Channel';
  constructor(private http: HttpClient) {}

 CreateChannel(ChannelData:any):Observable<any> {
    const RequestUrl= `${this.url}`;
    return this.http.post(RequestUrl,ChannelData);
  }

}
