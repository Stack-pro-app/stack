import { Channel } from "./channel";

export interface Workspace {
    id:Number,
    name:string,
    MainChannel:any,
    channels:Channel[],
}
