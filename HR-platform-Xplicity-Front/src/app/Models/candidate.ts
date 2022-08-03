export class Candidate{
    Id?: number;
    FirstName: string = "";
    LastName: string = "";
    LinkedIn: string = "";
    Comment: string= "";
    Technologies: string = ""; // add model for technology? make it as an array?
    DatesOfPastCalls: string = ""; 
    DateOfFutureCall: string = "";
    OpenForSuggestions: boolean = false;
}