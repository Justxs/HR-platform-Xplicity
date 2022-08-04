import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Candidate } from '../Models/candidate';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {

  private url = "candidates"
  private readonly candidateApi = `${environment.webApiUrl}/${url}`;

  constructor(private http: HttpClient) { }

  public createCandidate(candidate: Candidate): Observable<Candidate[]> {
    return this.http.post<Candidate[]>(this.candidateApi);

  }
  

  public getCandidate() : Observable<Candidate[]> {

    return this.http.get<Candidate[]>(this.candidateApi);

  }
}
