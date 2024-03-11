import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PostProComponent} from "./post-pro/post-pro.component";
import {DisplayProjectComponent} from "./display-project/display-project.component";
import {EditProjectComponent} from "./edit-project/edit-project.component";

const routes: Routes = [
  {path:"post",component:PostProComponent},
  {path : "",component:DisplayProjectComponent},
  {path : "edit/:id",component:EditProjectComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
