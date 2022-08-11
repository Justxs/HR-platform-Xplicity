import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tokenGetter } from '../app.module';
import { UserModel } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();
  user!:UserModel;

  constructor() {
   }

  IsLoggedIn(){
    return !!localStorage.getItem('Jwt');
  }
}
