import { TagDTO } from './TagDTO';
export interface TagRecord {
    Id: number,
    TagDTO: TagDTO,
    TagId: number,
    Timestamp: string,
    Value: number,
    IoAddress: string
}