export interface DigitalInput {
    id: number,
    name: string,
    ioAddress: string,
    description: string,
    value: number,
    driver: string,
    scanTime: number,
    scan: boolean
}
