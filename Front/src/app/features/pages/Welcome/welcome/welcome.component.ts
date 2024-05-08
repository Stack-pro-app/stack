import { Component, Type } from '@angular/core';
import { HeaderComponent } from '../../header/header.component';
import { RouterLink } from '@angular/router';
import { TypeWriterDirective } from '../../../../core/Directives/TypeWriterDirective';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [HeaderComponent, RouterLink, TypeWriterDirective],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent {

}
