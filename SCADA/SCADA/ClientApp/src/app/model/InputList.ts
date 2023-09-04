import { AnalogInput } from "./AnalogInput";
import { DigitalInput } from "./DigitalInput";

export interface InputList {
    digitalInputList: AnalogInput[],
    analogInputList: AnalogInput[]
}
