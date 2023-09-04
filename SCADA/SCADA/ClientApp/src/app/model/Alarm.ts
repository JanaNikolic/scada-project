import { AnalogInput } from './AnalogInput';
export interface Alarm {
    Id: number,
    Threshold: number,
    Type: string,
    Priority: string,
    AnalogInput: AnalogInput,
    TagId: number,
    Timestamp: string
}
