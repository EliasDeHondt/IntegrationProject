export class ChoiceBox {
    startPos: number;
    endPos: number;
    constructor(startPos: number, endPos: number) {
        this.startPos = startPos;
        this.endPos = endPos;
    }
    
    isInside(x: number): boolean{
        return x >= this.startPos && x <= this.endPos;
    }
    
}