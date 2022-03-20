export class SearchItemRequestDto {

    constructor(text: string, type: number) {
        this.text = text;
        this.type = type;
    }

    text: string = "";
    type: number = 0;
}