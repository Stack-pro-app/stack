import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  loggeduser:any = false;

  constructor(private store:StoreService) {
  
  }

  ngOnInit(): void {
        this.loggeduser = this.store.isLogged;
        console.log(this.loggeduser);
  }
  
}
