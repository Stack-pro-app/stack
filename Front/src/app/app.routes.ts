import { Routes } from '@angular/router';
import { LoginComponent } from './features/pages/login/login.component';
import { HomeComponent } from './features/pages/home/home.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { PageNotFoundComponent } from './features/pages/page-not-found/page-not-found.component';
import { WorkspacesDisplayComponent } from './features/pages/chat/workspaces-display/workspaces-display.component';
import { InsideWorkspaceComponent } from './features/pages/chat/inside-workspace/inside-workspace.component';
import { MainComponent } from './features/pages/main/main.component';
import { ProfileComponent } from './features/pages/profile/profile.component';
export const routes: Routes = [
    {path:'',pathMatch:'full',redirectTo:'Home'},
    {path:'Login',component:LoginComponent,title:'Login'},
    {path:'Home',component:HomeComponent,title:'Home'},
    {path:'Register',component:RegisterComponent,title:'Register'},
    {path:'Workspaces',component:WorkspacesDisplayComponent,title:'Workspaces'},
    {path:'Workspace/:id',component:InsideWorkspaceComponent,title:'Workspace'},
    {path:'Main',title:'main',component:MainComponent},
    {path:'Profile',title:'profile',component:ProfileComponent},
    {path:'**',pathMatch:'full',component:PageNotFoundComponent}
];
