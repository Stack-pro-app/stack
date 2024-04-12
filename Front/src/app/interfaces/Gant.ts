import {TaskInter1} from "./task-inter1";
import {taskGant} from "./taskGant";

export interface GantInter {
  id: number;
  projectName: string;
  projectDescrp: string;
  start: string;
  end: string;
  budget: number;
  clientName: string;
  tasks: taskGant[];

}
