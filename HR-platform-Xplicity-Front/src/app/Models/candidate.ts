import { Technology } from "./technology";

export class Candidate {
    id: number;
    firstName: string;
    lastName: string;
    linkedIn: string;
    comment: string;
    technologies: Technology[];
    datesOfPastCalls: Date[];
    dateOfFutureCall: Date;
    openForSuggestions: boolean;

    constructor() {
        this.id = 0;
        this.firstName = "";
        this.lastName = "";
        this.linkedIn = "";
        this.comment= "";
        this.technologies = [];
        this.datesOfPastCalls = [];
        this.dateOfFutureCall = new Date();
        this.openForSuggestions = false;
    }
}