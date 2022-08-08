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
  //value!: Date;

  dates: string[] = [];

  constructor(
    private candidateService: CandidateService,
    private technologyService: TechnologyService) {
    this.technologyService.getTechnologies()
      .subscribe(items => { this.technologies = items; });
  }

  onSubmit(form: NgForm): void {
    //form.resetForm();
  }

  ngOnInit(): void { }

  createCandidate(candidate: Candidate) {
    this.dates.forEach(date => {
      var callDate = new CallDate(date);
      candidate.pastCallDates.push(callDate);
    });
    this.candidateService.createCandidate(candidate)
      .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
  }

}
