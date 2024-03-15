import { Routes } from '@angular/router';
import { LoginComponent } from './features/pages/login/login.component';
import { HomeComponent } from './features/pages/home/home.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { PageNotFoundComponent } from './features/pages/page-not-found/page-not-found.component';

import { MainComponent } from './features/pages/main/main.component';
import { ProfileComponent } from './features/pages/profile/profile.component';
import { CreateWorkSpaceComponent } from './features/pages/create-work-space/create-work-space.component';
import { VideoCallComponent } from './features/pages/VideoCall/video-call/video-call.component';
import { FileUploadComponent } from './shared/components/file-upload/file-upload.component';
export const routes: Routes = [
    {path:'',pathMatch:'full',redirectTo:'Home'},
    {path:'Login',component:LoginComponent,title:'Login'},
    {path:'Home',component:HomeComponent,title:'Home'},
    {path:'Register',component:RegisterComponent,title:'Register'},
    {path:'Main/:id',title:'main',component:MainComponent},
    {path:'Profile',title:'profile',component:ProfileComponent},
    {path:'Create',title:'Create',component:CreateWorkSpaceComponent},
    {path:'Call/:ChannelString',title:'Call',component:VideoCallComponent},
    {path:'Drag',component:FileUploadComponent},
    {path:'**',pathMatch:'full',component:PageNotFoundComponent}
];
