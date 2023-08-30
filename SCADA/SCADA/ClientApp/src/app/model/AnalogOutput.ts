export interface AnalogOutput {
    id: number,
    name: string,
    ioAddress: string,
    description: string,
    value: number,
    initialValue: number,
    lowLimit: number,
    highLimit: number,
    units: string
}
