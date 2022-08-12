import { Technology } from '../../Models/technology';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Candidate } from '../../Models/candidate';
import { CandidateService } from '../../Services/candidate.service';
import { TechnologyService } from '../../Services/technology.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CallDate } from 'src/app/Models/callDate';

import { ConfirmationService, ConfirmEventType, FilterService, LazyLoadEvent, MessageService } from 'primeng/api';
import { NgForm } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import * as XLSX from 'XLSX';
import * as moment from 'moment';
import { ToastModule } from 'primeng/toast';
import { tokenGetter } from 'src/app/app.module';
import { UserService } from 'src/app/Services/user.service';

type AOA = any[][];

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css'],
  providers: [ConfirmationService, MessageService]

})

export default class TablePageComponent implements OnInit {
  @Output() candidatesUpdated = new EventEmitter<Candidate[]>();
  candidates: Candidate[] = [];
  candidateToEdit: Candidate = new Candidate();
  technologies: Technology[] = [];
  createCandidateDialog: boolean = false;
  updateCandidateDialog: boolean = false;
  createTechnologyDialog: boolean = false;
  submitted: boolean = false;
  newUserDialog: boolean = false;
  displayMenu: boolean = false;
  deleteDialog: boolean = false;
  showButton: boolean = false;
  status: string = "";
  currentRole:any;
  data: AOA = [[]];
  dates: string[] = [];
  invalidLogin: boolean | undefined;
  hid: boolean = true;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private candidateService: CandidateService,
    private technologyService: TechnologyService,
    private userService: UserService,
    private confirmationService: ConfirmationService,
    private filterService: FilterService,
    private http: HttpClient,
    private messageService: MessageService) { }

  ngOnInit(): void {
    const customFilterName = 'custom-filter';
    this.filterService.register(customFilterName, (value: any[], filter: any[]): boolean => {
      if (filter === undefined || filter === null) {
          return true;
      }
      if (value === undefined || value === null) {
          return false;
      }
      let count: number = 0;
      filter.forEach(fil => {
        value.forEach(val => {
          if(val.title == fil.title){
            count += 1;

          }
        })
      });
      if(count == filter.length){
        return true;
      }else{
        return false;
      }
  });

    this.hid = false;
    this.candidateService.getCandidate()
      .subscribe(
    items => {
      this.candidates = items;
      this.hid = true;
    });

    this.technologyService.getTechnologies()
      .subscribe(
        items => {
          this.technologies = items;
      });
  }

  menuDisplay(){
    if(tokenGetter()!='') {
      this.currentRole = this.userService.GetRoleByToken(tokenGetter())
      if(this.currentRole.includes()){
        this.displayMenu = true;
      }
      else{
        this.displayMenu = false;
      }
    }
  }

  deleteCandidate(candidate: Candidate) {
    this.hid = false;
    this.candidateService.deleteCandidate(candidate)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
      setTimeout(()=>{this.wait()},2000);
    this.showMessage("Kandidatas sėkmingai ištrintas", "success", "pavyko");
  }

  generateOffer(candidate: Candidate){
    this.hid = false;
    this.candidateService.generateOffer(candidate.firstName,candidate.lastName)
      .subscribe((response: Blob) =>{
          let blob = new Blob([response], { type: "application/vnd.openxmlformats-officedocument.wordprocessingml.document"});
          let a = document.createElement('a');
          a.href = window.URL.createObjectURL(blob);
          a.click();
          this.hid = true;
          this.showMessage("Darbo pasiūlymas sugeneruotas", "success", "pavyko");
      });
  }

  createCandidate(candidates: Candidate[]) {
    this.hid = false;
    this.candidateService.createCandidate(candidates)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
    setTimeout(()=>{this.wait()},2000);
    this.showMessage("Kandidatas sėkmingas įtrauktas", "success", "pavyko");
  }

  openNewTechnologyForm(){
    this.createTechnologyDialog = true;
  }
  wait(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['./'], {relativeTo: this.route});
  }
  openNewCandidateForm() {
    this.submitted = false;
    this.createCandidateDialog = true;
  }

  updateCandidateForm(candidate: Candidate) {
    this.submitted = false;
    this.updateCandidateDialog = true;
    this.candidateToEdit = candidate;
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
    localStorage.removeItem("Jwt"),
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
        if (value[1] == null || value[2] == null || value[3] == null || value[5] == null) {
          return;
        }
        json.push(formatJson(value));
      });
      json.splice(0, 1);
      this.createCandidate(json)
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
        setTimeout(()=>{this.wait()},1000);
        this.showMessage("Vartotojas sėkmingai pridetas", "success", "pavyko");
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
          setTimeout(()=>{this.wait()},1000);
          this.showMessage("Vartotojas sėkmingai ištrintas", "success", "pavyko");
        },
        error: error => {
          this.invalidLogin = true;
        }
      })

  }
  showMessage(message: string, severity: string, summary: string) {
    this.messageService.add({severity: severity, summary: summary, detail: message});
  }


}
function formatJson(value: any): Candidate {
  let JsonCandidate = new Candidate();
  if (value[0] == null) {
    JsonCandidate.pastCallDates = [];
  } else {
    let val: string[] = value[0].toString().split(/\r?\n/);
    JsonCandidate.pastCallDates = [];
    if(val.length > 1){
      val.forEach(va => {
        JsonCandidate.pastCallDates.push({ dateOfCall: va});
      });
    }else{
      JsonCandidate.pastCallDates.push({ dateOfCall: moment(getJsDateFromExcel(value[0])).format('YYYY-MM-DD')});
    }
  }
  JsonCandidate.firstName = value[1];
  JsonCandidate.lastName = value[2];
  JsonCandidate.linkedIn = value[3];
  JsonCandidate.comment = value[4];
  JsonCandidate.technologies = [];
  let val: string[] = value[5].split(/\r?\n/);
  val.forEach(va => {
    JsonCandidate.technologies.push({ title: va });
  });

  if (value[6] == null) {
    JsonCandidate.dateOfFutureCall = "";
  } else {
    JsonCandidate.dateOfFutureCall = moment(getJsDateFromExcel(value[6])).format('YYYY-MM-DD');
  }
  if (value[7] == "v") {
    JsonCandidate.openForSuggestions = true;
  } else {
    JsonCandidate.openForSuggestions = false;
  }
  return JsonCandidate;
}
function getJsDateFromExcel(excelDate: any) {
  return new Date((excelDate - (25567 + 1)) * 86400 * 1000);
}




