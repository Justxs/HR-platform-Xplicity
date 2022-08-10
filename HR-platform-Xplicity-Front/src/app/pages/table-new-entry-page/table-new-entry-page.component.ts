import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CallDate } from 'src/app/Models/callDate';
import { Candidate } from 'src/app/Models/candidate';
import { Technology } from 'src/app/Models/technology';
import { CandidateService } from 'src/app/Services/candidate.service';
import { TechnologyService } from 'src/app/Services/technology.service';
import * as moment from 'moment';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-table-new-entry-page',
  templateUrl: './table-new-entry-page.component.html',
  styleUrls: ['./table-new-entry-page.component.css',
  ]
})
export class TableNewEntryPageComponent implements OnInit {
  @Input() candidate: Candidate = new Candidate;
  @Output() candidatesUpdated = new EventEmitter<Candidate[]>();

  technologies: Technology[] = [];
  candidateTechnologies: [] = []

  dates: string[] = [];

  constructor(
    private candidateService: CandidateService,
    private technologyService: TechnologyService) {
   
  }

  onSubmit(form: NgForm, candidate:Candidate): void {
    this.createCandidate(candidate);

    //form.resetForm();
  }

  ngOnInit(): void { 
    this.technologyService.getTechnologies()
    .subscribe(items => { this.technologies = items; });
  }

  createCandidate(candidate: Candidate) {
    this.dates.forEach(date => {
      date = moment(date).format('YYYY-MM-DD');
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });
    var candidates: Candidate[] = [];
    if(candidate.firstName == "" || candidate.lastName == "" || candidate.linkedIn == ""){
      return;
    }
    candidates.push(candidate);
    this.candidateService.createCandidate(candidates)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
      window.location.reload();
  }

}
