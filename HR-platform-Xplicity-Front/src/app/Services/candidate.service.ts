import { Injectable } from '@angular/core';
import { Candidate } from '../Models/candidate';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {

  constructor() { }
  public getCandidate() : Candidate[] {
    let candidate = new Candidate();
    candidate.DateOfFutureCall = "test";
    candidate.DatesOfPastCalls = "test";
    candidate.FirstName = "test";
    candidate.LastName = "test";
    candidate.LinkedIn = "test";
    candidate.Technologies = "test";
    candidate.Comment = "test";
    candidate.OpenForSuggestions = true;
    let candidates = [];
    candidates.push(candidate);
    candidates.push(candidate);
    candidates.push(candidate);
    candidates.push(candidate);
    candidates.push(candidate);
    return candidates;
  }
}
