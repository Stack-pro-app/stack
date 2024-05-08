import { Injectable } from '@angular/core';
import { Channel } from '../../Models/channel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../enviromnents/enviroment';

@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  url: string = environment.API_MESAAGING_URL + '/api/Channel';
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
  GetChannelById(id: any): Observable<any> {
    const RequestUrl = `${this.url}/${id}`;
    return this.http.get(RequestUrl);
  }
}
