import { Injectable } from '@angular/core';
import { Channel } from '../../Models/channel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  url: string = 'http://localhost:8193/api/Channel';
  constructor(private http: HttpClient) {}

  CreateChannel(ChannelData: any): Observable<any> {
    const RequestUrl = `${this.url}`;
    return this.http.post(RequestUrl, ChannelData);
  }
  Delete(id: any): Observable<any> {
    const RequestUrl = `${this.url}/${id}`;
    return this.http.delete(RequestUrl);
  }
  Update(data: any): Observable<any> {
    const RequestUrl = `${this.url}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.put(RequestUrl, data, { headers });
  }
}
