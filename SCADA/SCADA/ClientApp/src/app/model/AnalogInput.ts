export interface AnalogInput {
    id: number,
    name: string,
    ioAddress: string,
    description: string,
    value: number,
    driver: string,
    scanTime: number,
    isScanOn: boolean,
    lowLimit: number,
    highLimit: number,
    units: string
}
