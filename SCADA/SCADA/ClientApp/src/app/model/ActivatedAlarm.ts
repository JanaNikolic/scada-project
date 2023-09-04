import { Alarm } from "./Alarm";

export interface ActivatedAlarm {
    Id: number,
    TimeStamp: string,
    Alarm: Alarm,
}