import { Technology } from '../../Models/technology';
import { Component, OnInit } from '@angular/core';
import { Candidate } from '../../Models/candidate';
import { CandidateService } from '../../Services/candidate.service';
import { TechnologyService } from '../../Services/technology.service';
import { Router } from '@angular/router';

import {ConfirmationService, ConfirmEventType, MessageService} from 'primeng/api';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css'],
  providers: [ConfirmationService,MessageService]

})
export class TablePageComponent implements OnInit {
  candidates: Candidate[] = [];
  technologies: Technology[] = [];
  candidateDialog: boolean = false;
  submitted: boolean = false;
  newUserDialog: boolean = false;
  deleteDialog: boolean = false;
  status: string = "";

  invalidLogin: boolean | undefined;

  constructor(private router: Router, private candidateService: CandidateService, private technologyService: TechnologyService,
    private confirmationService: ConfirmationService, private http: HttpClient) { }

  ngOnInit(): void {
    this.candidateService.getCandidate()
      .subscribe(
        items => {
          this.candidates = items;
        });

    this.technologyService.getTechnologies()
      .subscribe(
        items => {
          this.technologies = items;
        });
        
  }
  openNew() {
    this.submitted = false;
    this.candidateDialog = true;
}

createNewUser() {
  this.submitted = false;
  this.newUserDialog = true;
}

deleteUser() {
  this.submitted = false;
  this.deleteDialog = true;
}

logout(){
  localStorage.removeItem("jwt"),
  this.router.navigate([""])
}

register(form: NgForm){
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

delete(form: NgForm){
  const credentials = {
    'email': form.value.email
  }
  this.http.delete("https://localhost:7241/api/auth/delete", credentials.email)
    .subscribe({
      next: data => {
          this.status = 'Delete successful';
      },
      error: error => {
        this.invalidLogin = true;
      }
  })
}
}