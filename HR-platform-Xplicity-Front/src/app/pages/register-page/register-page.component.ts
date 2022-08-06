import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {

  invalidLogin: boolean | undefined;

constructor(private router: Router, private http: HttpClient){}

login(form: NgForm){
  const credentials = {
    'email': form.value.email,
    'password': form.value.password
  }
  this.http.post("https://localhost:7241/api/auth/register", credentials)
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