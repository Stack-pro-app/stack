import { Component } from '@angular/core';
import { HeaderComponent } from '../../header/header.component';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [HeaderComponent, RouterLink],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent {

}
