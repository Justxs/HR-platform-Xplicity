import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CallDate } from 'src/app/Models/callDate';
import { Candidate } from 'src/app/Models/candidate';
import { Technology } from 'src/app/Models/technology';
import { CandidateService } from 'src/app/Services/candidate.service';
import { TechnologyService } from 'src/app/Services/technology.service';
import * as moment from 'moment';
import {MessageService} from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-table-new-entry-page',
  templateUrl: './table-new-entry-page.component.html',
  styleUrls: ['./table-new-entry-page.component.css',
  ]
})
export class TableNewEntryPageComponent implements OnInit {
  @Input() candidate: Candidate = new Candidate;
  @Input() existingCandidates: Candidate[] = [];
  @Output() candidatesUpdated = new EventEmitter<Candidate[]>();

  technologies: Technology[] = [];
  candidateTechnologies: [] = []

  dates: string[] = [];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private candidateService: CandidateService,
    private technologyService: TechnologyService,
    private messageService: MessageService) {}

  onSubmit(form: NgForm, candidate:Candidate): void {
    this.createCandidate(candidate);

  }

  ngOnInit(): void { 
    this.technologyService.getTechnologies()
    .subscribe(items => { this.technologies = items; });

  }

  createCandidate(candidate: Candidate) {
    if(candidate.firstName == "" || candidate.lastName == "" || candidate.linkedIn == ""){
      this.showToast("Ne visi privalomi laukai užpildyti",'error','Klaida');
      return;
    }

    var exists = false;
    this.existingCandidates.forEach(c => {
      if (candidate.firstName == c.firstName
        && candidate.lastName == c.lastName
        && candidate.linkedIn == c.linkedIn) {
          exists = true;
        }
    });

    if (exists) {
      this.showToast('Kandidatas jau egzistuoja', 'error', 'Klaida');
      return;
    }

    this.dates.forEach(date => {
      date = moment(date).format('YYYY-MM-DD');
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });

    var candidates: Candidate[] = [];

    candidates.push(candidate);
    this.candidateService.createCandidate(candidates)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
    setTimeout(()=>{this.wait()},1500);
    this.showToast("Pavyko pridėti kandidatą", 'success', 'Pavyko');
  }
  
  wait(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['./'], {relativeTo: this.route});
  }

  updateCandidate(candidate: Candidate) {
    this.dates.forEach(date => {
      date = moment(date).format('YYYY-MM-DD');
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });
    if(candidate.firstName == "" || candidate.lastName == "" || candidate.linkedIn == ""){
      this.showToast("Ne visi privalomi laukai užpildyti",'error','Klaida');
      return;
    }
    candidate.dateOfFutureCall = moment(candidate.dateOfFutureCall).format('YYYY-MM-DD');
    this.candidateService.updateCandidate(candidate)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
      //setTimeout(()=>{this.wait()},2000);
      this.showToast("Pavyko pakeisti kandidato duomenis", 'success', 'Pavyko');
  }
  showToast(message: string, severity: any, summary: any) {
    this.messageService.add({severity: severity, summary: summary, detail: message});
  }

}
