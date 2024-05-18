import { Component } from '@angular/core';
import { GanttModule } from '@syncfusion/ej2-angular-gantt';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './features/pages/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [GanttModule, RouterOutlet,HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Front';

}

