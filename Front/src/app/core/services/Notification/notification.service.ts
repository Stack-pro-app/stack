import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppConfig } from '../../../app.custom.config';
import { Notification } from '../../Models/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  url: string = `${AppConfig.apiUrl}/Notification`;
  constructor(private httpClient:HttpClient) { }
  GetUnSeenNotifications(notifString:string):Observable<any>{
    return this.httpClient.get(`${this.url}/Unseen/${notifString}`);
  }
  GetSeenNotifications(notifString:string):Observable<any>{
    return this.httpClient.get(`${this.url}/Seen/${notifString}?page=1`);
  }
}
