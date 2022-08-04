export class Candidate{
    id?: number;
    firstName: string = "";
    lastName: string = "";
    linkedIn: string = "";
    comment: string = "";
    technologies: string = ""; // add model for technology? make it as an array?
    datesOfPastCalls: Date[] = []; 
    dateOfFutureCall?: Date;
    openForSuggestions: boolean = false;
}