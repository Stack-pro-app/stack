import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  url: string = 'http://localhost:8193/api/Chat';
  constructor(private http: HttpClient) {}

  GetMessages(ChannelId: any, id: any): Observable<any> {
    const RequestUrl = `${this.url}/channel/${ChannelId}?page=${id}`;
    return this.http.get(RequestUrl);
  }
  SendMessage(data: any): Observable<any> {
    console.log(data);
    const CreateUrl = `${this.url}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post(CreateUrl, data, { headers });
  }
}
