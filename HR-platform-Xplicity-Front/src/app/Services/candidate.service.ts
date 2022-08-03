import { Injectable } from '@angular/core';
import { Candidate } from '../Models/candidate';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  private readonly candidateApi = `${environment.webApiUrl}/Candidates`;

  constructor(private http: HttpClient) { }

  public getCandidate() : Observable<Candidate[]> {

    return this.http.get<Candidate[]>(this.candidateApi);
  }
}
