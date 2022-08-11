export class CallDate {
    id?: number;
    public dateOfCall : string;

    //public constructor()
    //{
    //    this.id = 0;
    //    this.dateOfCall =  "";
    //}

    constructor(theDate: string) {
        this.dateOfCall = theDate;
    }
}