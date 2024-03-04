import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MessageComponent } from '../message/message.component';

@Component({
  selector: 'app-channel',
  standalone: true,
  imports: [CommonModule,MessageComponent],
  templateUrl: './channel.component.html',
  styleUrl: './channel.component.css'
})
export class ChannelComponent implements OnInit {

  messages : any[]=[];
  //add the messaging service here
  constructor(){

  }
  ngOnInit(): void {
      
  }
}
