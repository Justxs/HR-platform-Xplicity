export class Candidate{
    Id?: number;
    firstName: string = "";
    lastName: string = "";
    linkedIn: string = "";
    comment: string = "";
    technologies: string = ""; // add model for technology? make it as an array?
    dateOfPastCalls: string = ""; 
    dateOfFutureCall: string = "";
    openForSuggestions: boolean = false;
}