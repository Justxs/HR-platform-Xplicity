import { Technology } from './technology';
import { CallDate } from './callDate';

export class Candidate {
    id: number;
    firstName: string;
    lastName: string;
    linkedIn: string;
    comment: string;
    technologies: Technology[];
    pastCallDates: CallDate[];
    dateOfFutureCall: Date;
    openForSuggestions: boolean;

    technologyDisplay?: string;
    datesOfPastCallsDisplay?: string;

    constructor() {
        this.id = 0;
        this.firstName = "";
        this.lastName = "";
        this.linkedIn = "";
        this.comment= "";
        this.technologies = [];
        this.pastCallDates = [];
        this.dateOfFutureCall = new Date();
        this.openForSuggestions = false;
    }
    
}