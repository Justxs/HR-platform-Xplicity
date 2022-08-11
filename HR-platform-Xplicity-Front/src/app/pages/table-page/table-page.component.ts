import { Technology } from '../../Models/technology';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Candidate } from '../../Models/candidate';
import { CandidateService } from '../../Services/candidate.service';
import { TechnologyService } from '../../Services/technology.service';
import { Router } from '@angular/router';
import { CallDate } from 'src/app/Models/callDate';

import { ConfirmationService, ConfirmEventType, MessageService } from 'primeng/api';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import * as XLSX from 'XLSX';
import * as moment from 'moment';
import { ToastModule } from 'primeng/toast';
import { from } from 'rxjs';

type AOA = any[][];

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css'],
  providers: [ConfirmationService, MessageService]

})

export class TablePageComponent implements OnInit {
  @Output() candidatesUpdated = new EventEmitter<Candidate[]>();
  candidates: Candidate[] = [];
  candidateToEdit: Candidate = new Candidate();
  technologies: Technology[] = [];
  createCandidateDialog: boolean = false;
  updateCandidateDialog: boolean = false;
  submitted: boolean = false;
  newUserDialog: boolean = false;
  deleteDialog: boolean = false;
  status: string = "";

  data: AOA = [[]];
  dates: string[] = [];
  invalidLogin: boolean | undefined;

  constructor(private router: Router, 
    private candidateService: CandidateService, 
    private technologyService: TechnologyService,
    private confirmationService: ConfirmationService, 
    private http: HttpClient, 
    private messageService: MessageService) { }

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

  deleteCandidate(candidate: Candidate) {
    this.candidateService.deleteCandidate(candidate)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
    window.location.reload();
  }

  createCandidate(candidates: Candidate[]) {
    this.candidateService.createCandidate(candidates)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
      //window.location.reload();
  }

  openNewCandidateForm() {
    this.submitted = false;
    this.createCandidateDialog = true;
  }

  updateCandidateForm(candidate: Candidate) {
    this.submitted = false;
    this.updateCandidateDialog = true;
  }

  createNewUser() {
    this.submitted = false;
    this.newUserDialog = true;
  }

  deleteUser() {
    this.submitted = false;
    this.deleteDialog = true;
  }

  logout() {
    localStorage.removeItem("jwt"),
      this.router.navigate([""])
  }

  myUploader(event: any) {
    const target: DataTransfer = <DataTransfer>(event);
    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];


      this.data = <AOA>(XLSX.utils.sheet_to_json(ws, { header: 1 }));
      let json: Candidate[] = [];
      this.data.forEach(function (value) {
        if (value[1] == null) {
          return;
        }
        json.push(formatJson(value));
      });
      json.splice(0, 1);
      console.log(JSON.stringify(json));
    };
    reader.readAsBinaryString(target.files[0]);
  };


  create(form: NgForm) {
    const credentials = {
      'email': form.value.email,
      'password': form.value.password,
    }
    this.http.post("https://localhost:7241/api/auth/create", credentials)
      .subscribe(response => {
        const token = (<any>response).token;
        localStorage.setItem("Jwt", token);
        this.invalidLogin = false;
        this.router.navigate(["/table"])
        console.log()
      }, err => {
        this.invalidLogin = true;
      })
  }
  
  delete(form: NgForm) {
    const credentials = {
      'email': form.value.email
    }
    this.http.delete("https://localhost:7241/api/auth/" + credentials.email)
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
function formatJson(value: any): Candidate{
  let JsonCandidate = new Candidate();
  JsonCandidate.pastCallDates = [{dateOfCall: moment(getJsDateFromExcel(value[0]).toLocaleString()).format('YYYY-MM-DD')}];
  JsonCandidate.firstName = value[1];
  JsonCandidate.lastName = value[2];
  JsonCandidate.linkedIn = value[3];
  JsonCandidate.comment = value[4];
  JsonCandidate.technologies = [{title: value[5]}];
  JsonCandidate.dateOfFutureCall = moment(getJsDateFromExcel(value[6])).format('YYYY-MM-DD');
  if(value[7] == "v"){
    JsonCandidate.openForSuggestions = true;
  } else{
    JsonCandidate.openForSuggestions = false;
  }
  return JsonCandidate;
}
function getJsDateFromExcel(excelDate: any) {
	return new Date((excelDate - (25567 + 1))*86400*1000);
}


