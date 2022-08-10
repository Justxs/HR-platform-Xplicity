import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CallDate } from 'src/app/Models/callDate';
import { Candidate } from 'src/app/Models/candidate';
import { Technology } from 'src/app/Models/technology';
import { CandidateService } from 'src/app/Services/candidate.service';
import { TechnologyService } from 'src/app/Services/technology.service';


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
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });


    console.log(candidate.technologies);
    this.candidateService.createCandidate(candidate)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
  }

}
