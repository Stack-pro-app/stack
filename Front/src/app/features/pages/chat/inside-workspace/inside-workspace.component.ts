import { Component } from '@angular/core';

@Component({
  selector: 'app-inside-workspace',
  standalone: true,
  imports: [],
  templateUrl: './inside-workspace.component.html',
  styleUrl: './inside-workspace.component.css'
})
export class InsideWorkspaceComponent {
  channel:any = {
    Name: 'Channel 1',
    Description: 'This is the first channel',
  };


}
