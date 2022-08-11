import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/Models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  
invalidLogin: boolean | undefined;
  user: any;

constructor(private router: Router, 
  private http: HttpClient,
  ){}

login(form: NgForm){
  const credentials = {
    'email': form.value.email,
    'password': form.value.password
  }
  this.http.post("https://localhost:7241/api/auth/login", credentials)
    .subscribe(response =>{
      const token = (<any>response).token;
      localStorage.setItem("Jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["/table"])
    }, err => {
      this.invalidLogin = true;
    })
}
}
