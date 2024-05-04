import {TaskInter1} from "./task-inter1";

export interface UserInter {
  id?: number;
  userName: string;
  AuthId: string;
  email?: string;
  tasks?: TaskInter1[];
}

