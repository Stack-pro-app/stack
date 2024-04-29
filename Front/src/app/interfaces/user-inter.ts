import {TaskInter1} from "./task-inter1";

export interface UserInter {
  id?: number;
  userName: string;
  role: number;
  email?: string;
  tasks?: TaskInter1[];
}

