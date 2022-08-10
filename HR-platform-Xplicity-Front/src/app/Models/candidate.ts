import { Technology } from './technology';
import { CallDate } from './callDate';

export class Candidate {
    id?: number;
    firstName: string;
    lastName: string;
    linkedIn: string;
    comment: string;
    technologies: Technology[];
    pastCallDates: CallDate[];
    dateOfFutureCall: string;
    openForSuggestions: boolean;

    technologyDisplay?: string;
    datesOfPastCallsDisplay?: string;

    constructor() {
        this.firstName = "";
        this.lastName = "";
        this.linkedIn = "";
        this.comment= "";
        this.technologies = [];
        this.pastCallDates = [];
        this.dateOfFutureCall = "";
        this.openForSuggestions = false;
    }
    
}