import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PostProComponent} from "./post-pro/post-pro.component";
import {DisplayProjectComponent} from "./display-project/display-project.component";
import {EditProjectComponent} from "./edit-project/edit-project.component";
import {ListTasksComponent} from "./components/list-tasks/list-tasks.component";
import {AddTaskComponent} from "./components/add-task/add-task.component";
import {UsersComponent} from "./components/users/users.component";
import {GanttComponent} from "./components/gantt/gantt.component";

const routes: Routes = [
  {path:"gg",component:GanttComponent},
  {path:"post",component:PostProComponent},
  {path : "",component:DisplayProjectComponent},
  {path : "edit/:id",component:EditProjectComponent},

  {path : "tsk",component:ListTasksComponent},
  {path : "users",component:UsersComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
