import { Routes } from '@angular/router';
import { LoginComponent } from './features/pages/login/login.component';
import { HomeComponent } from './features/pages/home/home.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { PageNotFoundComponent } from './features/pages/page-not-found/page-not-found.component';

export const routes: Routes = [
    {path:'',pathMatch:'full',redirectTo:'Login'},
    {path:'Login',component:LoginComponent,title:'Login'},
    {path:'Home',component:HomeComponent,title:'Home'},
    {path:'Register',component:RegisterComponent,title:'Register'},
    {path:'**',pathMatch:'full',component:PageNotFoundComponent}
];
