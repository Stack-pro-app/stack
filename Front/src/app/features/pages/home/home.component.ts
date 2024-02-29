import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { AuthService } from '../../../core/services/Auth/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  loggeduser:any = '';

  constructor(private store:StoreService,
    private service:AuthService
    ) {
  
  }

  ngOnInit(): void {
        this.loggeduser = this.store.isLogged();
        console.log(this.loggeduser);
  }
  OnLogout(){
      this.service.logout();
  }
}
