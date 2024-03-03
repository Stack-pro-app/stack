import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-workspaces-display',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './workspaces-display.component.html',
  styleUrl: './workspaces-display.component.css'
})
export class WorkspacesDisplayComponent {
  workspaces:any = [
    {
      Name: 'Workspace 1',
      Description: 'This is the first workspace',
    },
    {
      Name: 'Workspace 2',
      Description: 'This is the second workspace',
    }
  ];

}
