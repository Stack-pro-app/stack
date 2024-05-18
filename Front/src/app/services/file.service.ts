import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType } from  '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  serverUrl: string = 'http://localhost:8091/api/File';
  constructor(private httpClient: HttpClient) {
   }

   fileSent: boolean = false;
   uploadFile(file: File, channelString: string, userId: string, channelId: number, message?: string): Observable<any> {
    const formData = new FormData();
    formData.append('channelString', channelString);
    formData.append('userId', userId);
    formData.append('channelId', channelId.toString());
    if (message) {
      formData.append('message', message);
    }
    formData.append('formFile', file);

    return this.httpClient.post<any>(this.serverUrl, formData);
  }
}
