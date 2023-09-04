export interface TagDTO {
    type: string,
    id: number | null,
    name: string,
    ioAddress: string,
    description: string,
    value: number,
    driver: string | null,
    scanTime: number | null,
    isScanOn: boolean | null,
    lowLimit: number | null,
    highLimit: number | null,
    units: string | null,
    initialValue: number | null
}
