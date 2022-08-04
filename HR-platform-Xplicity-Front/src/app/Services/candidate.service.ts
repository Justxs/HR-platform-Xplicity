import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Candidate } from '../Models/candidate';


@Injectable({
  providedIn: 'root'
})
export class CandidateService {

  private readonly candidateApi = `${environment.webApiUrl}/candidates`;

  constructor(private http: HttpClient) { }

  public createCandidate(candidate: Candidate): Observable<Candidate[]> {
    return this.http.post<Candidate[]>(this.candidateApi, candidate);
  }
  

  public getCandidate() : Observable<Candidate[]> {
    return this.http.get<Candidate[]>(this.candidateApi);
  }
}
