interface Subtask {
  TaskID: number;
  TaskName: string;
  StartDate: Date;
  Duration: number;
  Progress: number;
  Predecessor?:string
}
