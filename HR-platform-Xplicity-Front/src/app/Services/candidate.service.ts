import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Candidate } from '../Models/candidate';
import { CallDate } from '../Models/callDate';
import { Person } from '../Models/person';


@Injectable({
  providedIn: 'root'
})
export class CandidateService {

  private readonly candidateApi = `${environment.webApiUrl}/candidates`;

  constructor(private http: HttpClient) { }

  public createCandidate(candidate: Candidate[]): Observable<Candidate[]> {
    return this.http.post<Candidate[]>(this.candidateApi, candidate);
  }
  public getCandidateByDate(date:string): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(`${this.candidateApi}/${date}`);
  }
  public updateCandidate(candidate: Candidate): Observable<Candidate[]> {
    return this.http.put<Candidate[]>(this.candidateApi, candidate);
  }
  public deleteCandidate(candidate: Candidate): Observable<Candidate[]> {
    return this.http.delete<Candidate[]>(`${this.candidateApi}/${candidate.id}`);
  }

  public generateOffer(firstname: string, lastName: string): Observable<any> {
    var body = new Person();
    body.name = firstname;
    body.lastName = lastName;
    const requestOptions : any = {
        observe: "response",
        responseType: "blob",           
        headers: new HttpHeaders({
          "Accept": "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        })
    };
    return this.http.post(`${environment.webApiUrl}/offer`, body, requestOptions);
  }

  public initCandidateTech(Candidates: Candidate[]){
    Candidates.forEach(candidate => {
      candidate.technologyDisplay = candidate.technologies.map(t => t.title).join(", ");
      candidate.datesOfPastCallsDisplay = candidate.pastCallDates.map(t => t.dateOfCall).join("; ");
    })
  }

  public getCandidate() : Observable<Candidate[]> {
    return this.http.get<Candidate[]>(this.candidateApi).pipe(
      tap(candidates => this.initCandidateTech(candidates))
    );
  }

}
