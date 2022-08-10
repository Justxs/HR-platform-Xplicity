import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly authApi = `${environment.webApiUrl}/auth`;

  constructor(private http: HttpClient) { }

  public createUser(user: User): Observable<User[]> {
    return this.http.post<User[]>(this.authApi, user);
  }
}
  