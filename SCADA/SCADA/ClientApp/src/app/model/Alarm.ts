import {AnalogInput} from "./AnalogInput";
import {DigitalInput} from "./DigitalInput";

export interface Alarm{
  id: number,
  type: AlarmType,
  priority: AlarmPriority,
  threshold: number,
  analogInputId: number,
  units: string
}

enum AlarmType { ABOVE, BELOW }

enum AlarmPriority { Level1, Level2, Level3}

export interface InputsDTO {
  analogInputList: AnalogInput[],
  digitalInputList: DigitalInput[]
}

export interface AlarmDTO {
  threshold: number,
  type: string,
  priority: string,
  tagId: number
}
