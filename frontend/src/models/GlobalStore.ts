import { SearchItemDto } from "./SearchItemDto"

export class GlobalStore {
    market: string = "";
    limit: string = "";
    genres: Array<string> = [];
    artists: Array<SearchItemDto> = [];
    tracks: Array<SearchItemDto> = [];
}