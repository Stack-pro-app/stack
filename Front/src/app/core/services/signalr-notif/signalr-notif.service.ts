import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { AppConfig } from '../../../app.custom.config';

@Injectable({
  providedIn: 'root',
})
export class SignalrNotifService {
  private hubConnection: signalR.HubConnection;
  private messageReceivedSubject = new Subject<any>();
  private sendSubject = new Subject<string>();

  messageReceived$: Observable<any> =
    this.messageReceivedSubject.asObservable();
  send$: Observable<string> = this.sendSubject.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${AppConfig.serverUrl}/notificationHub`)
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
  receiveMessage(): Observable<any> {
    return new Observable<any>((observer) => {
      this.hubConnection.on('notificationReceived', (message: string) => {
        try {
          observer.next(message);
        } catch (error) {
          console.error('Error parsing JSON message:', error);
          observer.error(error);
        }
      });
    });
  }

  // Method to join a group
  joinGroup(notifString: string): void {
    // Send a request to the SignalR hub to add the client to a group (userId)
    this.hubConnection
      .send('JoinGroup', notifString)
      .then(() => console.log('Joined group:', notifString)) // Log success message if joining group is successful
      .catch((error) =>
        console.error('Error joining group:', error) // Log error if joining group fails
      );
  }

  // Method to leave a group
  leaveGroup(notifString: string): void {
    // Send a request to the SignalR hub to remove the client from a group (userId)
    this.hubConnection
      .send('LeaveGroup', notifString)
      .then(() => console.log('Left group:', notifString)) // Log success message if leaving group is successful
      .catch((error) =>
        console.error('Error leaving group:', error) // Log error if leaving group fails
      );
  }
}

