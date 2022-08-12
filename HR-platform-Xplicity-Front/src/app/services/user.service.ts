import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { tokenGetter } from '../app.module';
import { User } from '../Models/user';
import { TokenConstants } from './token-claim-string';


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

  GetRoleByToken(token:string){
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token);
    console.log(decodedToken[TokenConstants.roleType])
    return decodedToken[TokenConstants.roleType];
  }


}

  