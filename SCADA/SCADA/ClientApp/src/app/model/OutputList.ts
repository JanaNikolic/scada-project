import { AnalogOutput } from "./AnalogOutput";
import { DigitalOutput } from "./DigitalOutput";

export interface OutputList {
    digitalOutputList: AnalogOutput[],
    analogOutputList: AnalogOutput[]
}
