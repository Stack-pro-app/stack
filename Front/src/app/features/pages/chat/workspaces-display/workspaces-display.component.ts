import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../../../../core/services/Chat/chat.service';

@Component({
  selector: 'app-workspaces-display',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './workspaces-display.component.html',
  styleUrl: './workspaces-display.component.css'
})
export class WorkspacesDisplayComponent implements OnInit{
  workspaces:any = [];

  constructor(private chatService:ChatService) { }

  ngOnInit() {
    this.chatService.GetUserWorkspace(1).subscribe((data:any) => {
      console.log(data);
      this.workspaces = data;
    })
  }


}
