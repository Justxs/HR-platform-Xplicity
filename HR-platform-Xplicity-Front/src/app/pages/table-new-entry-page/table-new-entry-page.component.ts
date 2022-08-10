import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CallDate } from 'src/app/Models/callDate';
import { Candidate } from 'src/app/Models/candidate';
import { Technology } from 'src/app/Models/technology';
import { CandidateService } from 'src/app/Services/candidate.service';
import { TechnologyService } from 'src/app/Services/technology.service';
import * as moment from 'moment';
import {MessageService} from 'primeng/api';

@Component({
  selector: 'app-table-new-entry-page',
  templateUrl: './table-new-entry-page.component.html',
  styleUrls: ['./table-new-entry-page.component.css',
  ]
})
export class TableNewEntryPageComponent implements OnInit {
  @Input() 
  candidate: Candidate = new Candidate;
  update: boolean = false;
  @Output() candidatesUpdated = new EventEmitter<Candidate[]>();

  technologies: Technology[] = [];
  candidateTechnologies: [] = []

  dates: string[] = [];

  constructor(
    private candidateService: CandidateService,
    private technologyService: TechnologyService,
    private messageService: MessageService) {}

  onSubmit(form: NgForm, candidate:Candidate): void {
    this.createCandidate(candidate);

    //form.resetForm();
  }

  ngOnInit(): void { 
    this.technologyService.getTechnologies()
    .subscribe(items => { this.technologies = items; });

    console.log(this.candidate);
  }

  createCandidate(candidate: Candidate) {
    this.dates.forEach(date => {
      date = moment(date).format('YYYY-MM-DD');
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });
    var candidates: Candidate[] = [];
    if(candidate.firstName == "" || candidate.lastName == "" || candidate.linkedIn == ""){
      this.showError();
      return;
    }
    candidates.push(candidate);
    this.candidateService.createCandidate(candidates)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
      window.location.reload();
  }
  

  updateCandidate(candidate: Candidate) {
    this.dates.forEach(date => {
      date = moment(date).format('YYYY-MM-DD');
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });
    if(candidate.firstName == "" || candidate.lastName == "" || candidate.linkedIn == ""){
      this.showError();
      return;
    }

    this.candidateService.updateCandidate(candidate)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
      window.location.reload();
  }
  showError() {
    this.messageService.add({severity:'error', summary: 'Klaida', detail: 'Ne visi privalomi laukai užpildyti'});
  }


}
