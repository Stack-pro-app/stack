import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection: signalR.HubConnection;
  private messageReceivedSubject = new Subject<any>();
  private sendSubject = new Subject<string>();

  messageReceived$: Observable<any> =
    this.messageReceivedSubject.asObservable();
  send$: Observable<string> = this.sendSubject.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:8091/channelHub')
      .build();

   
    this.hubConnection.on('Send', (message) => {
      console.log('Message sent : ', message);

      this.sendSubject.next(message);
    });
  }
  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }
  receiveMessage(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('messageReceived', (Jsonmessage: string) => {
        const message = JSON.parse(Jsonmessage);
        observer.next(message);
        console.log(message);
      });
    });
  }
  sendMessage(message: any): void {
    const messageJson = JSON.stringify(message);
    console.log(messageJson);

    this.hubConnection
      .send('SendMessage', messageJson)
      .then(() => console.log('Message Sent Success'))
      .catch((error) => console.error('error sending the message', error));
  }

  joinChannel(channel: string): void {
    this.hubConnection
      .send('AddToGroup', channel)
      .then(() => console.log('Joined channel:', channel))
      .catch((error) => console.error('Error joining channel:', error));
  }

  leaveChannel(channel: string): void {
    this.hubConnection
      .send('RemoveFromGroup', channel)
      .then(() => console.log('Left channel:', channel))
      .catch((error) => console.error('Error leaving channel:', error));
  }
}
