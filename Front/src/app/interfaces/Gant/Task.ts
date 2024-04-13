
import "../Gant/Subtask"
export interface Task {
  TaskID: number;
  TaskName: string;
  StartDate: Date;
  EndDate: Date;
  subtasks?: Subtask[];

}
