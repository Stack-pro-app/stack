import { Component, OnInit, Type } from '@angular/core';
import { HeaderComponent } from '../../header/header.component';
import { RouterLink } from '@angular/router';
import { TypeWriterDirective } from '../../../../core/Directives/TypeWriterDirective';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [HeaderComponent, RouterLink, TypeWriterDirective, CommonModule],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent implements OnInit {
  isLogged:boolean = false;
  ngOnInit(): void {
  this.isLogged = localStorage.getItem('token') ? true : false;
  }

}
