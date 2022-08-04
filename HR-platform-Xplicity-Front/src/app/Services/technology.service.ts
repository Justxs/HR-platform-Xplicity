import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Technology } from '../Models/technology';


@Injectable({
  providedIn: 'root'
})
export class TechnologyService {

  private readonly technologyApi = `${environment.webApiUrl}/technologies`;

  constructor(private http: HttpClient) { }

  public createCandidate(technology: Technology): Observable<Technology[]> {
    return this.http.post<Technology[]>(this.technologyApi, technology);
  }
  

  public getTechnologies() : Observable<Technology[]> {
    return this.http.get<Technology[]>(this.technologyApi);
  }
}
