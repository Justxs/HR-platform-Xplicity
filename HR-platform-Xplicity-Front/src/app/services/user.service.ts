import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly authApi = `${environment.webApiUrl}/auth`;
  tokenresp: any;

  constructor(private http: HttpClient) { }

  public createUser(user: User): Observable<User[]> {
    return this.http.post<User[]>(this.authApi, user);
  }

  GetRoleByToken(token:any){
    let _token=token.split('.')[1];
    this.tokenresp=JSON.parse(atob(_token))
    console.log(this.tokenresp.role)
    return this.tokenresp.role
  }
}
  