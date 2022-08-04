import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Candidate } from '../Models/candidate';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  private url = "candidates"

  constructor(private http: HttpClient) { }

  public createCandidate(candidate: Candidate): Observable<Candidate[]> {
    return this.http.post<Candidate[]>(`${environment.webApiUrl}/${this.url}`, candidate);

  }

  public getCandidate(): Candidate[] {
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
