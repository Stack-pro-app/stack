import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../../services/user.service";
import {userProgress} from "../../../../interfaces/userProgress";

import {UserInter} from "../../../../interfaces/user-inter";

@Component({
  selector: 'app-user-progress',
  templateUrl: './user-progress.component.html',
  styleUrl: './user-progress.component.css'
})
export class UserProgressComponent implements OnInit{
  arrUsers?: UserInter[];
  Users : userProgress[]=[]
  constructor(private srv:UserService) {

  }

  ngOnInit(): void {
    this.srv.getWithTsk().subscribe({
      next:value => {
        this.arrUsers=value;
        for(let tmp of this.arrUsers ){
          let user : userProgress = {userName:tmp.userName,toDo:0,inProgress:0,missed:0,done:0,progress:0};
          // @ts-ignore
          for(let tsk of tmp.tasks){
            // @ts-ignore
            user.progress+=tsk.status ;
            if(tsk.status==100){
              user.done++;
            } else if (this.isBeforeToday(tsk.start)){
              user.toDo++;
            } else if(this.isAfterDead(tsk.end)){
              user.missed++;
            } else{
              user.inProgress++;
            }
          }
          let totalTasks =user.inProgress+user.done+user.missed;

          if(totalTasks!=0){
            user.progress = user.progress/totalTasks;
            user.progress = Number(user.progress.toFixed(2));
          }
          this.Users.push(user);
        }
      }
    })
  }
  isBeforeToday(dateStr: string): boolean {
    const today = new Date();
    const date = new Date(dateStr);


    today.setHours(0, 0, 0, 0);
    date.setHours(0, 0, 0, 0);
    /* console.log("date"+date);
     console.log("today"+today);
     console.log(date >= today);*/
    return date > today;
  }
  isAfterDead(endDateStr: string): boolean {
    const today = new Date();
    const endDate = new Date(endDateStr);



    today.setHours(0, 0, 0, 0);
    endDate.setHours(0, 0, 0, 0);

    return endDate < today;
  }

}
