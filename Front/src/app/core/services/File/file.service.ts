import { Injectable } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {
 url:string = "http://localhost:8091/api/File";
  constructor(private http : HttpClient) { }
}
