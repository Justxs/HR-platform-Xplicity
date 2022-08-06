import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import { Observable } from 'rxjs';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  private readonly userApi = `${environment.webApiUrl}/users`;

  private readonly httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json '})
  };

  constructor(private http: HttpClient) { }

  getUser(): Observable<User[]> {
    return this.http.get<User[]>(this.userApi);
  }

  // addProduct(user: User): Observable<User> {
  //   return this.http.post<User>(this.userApi, user, this.httpOptions);
  // }
}
