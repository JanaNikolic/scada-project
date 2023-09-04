import { Alarm } from "./Alarm";

export interface ActivatedAlarm {
    Id: number,
    Timestamp: string,
    Alarm: Alarm,
    AlarmId: number,
}
