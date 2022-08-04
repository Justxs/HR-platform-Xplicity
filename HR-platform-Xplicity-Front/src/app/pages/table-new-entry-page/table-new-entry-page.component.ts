import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Candidate } from 'src/app/Models/candidate';
import { CandidateService } from 'src/app/Services/candidate.service';

@Component({
  selector: 'app-table-new-entry-page',
  templateUrl: './table-new-entry-page.component.html',
  styleUrls: ['./table-new-entry-page.component.css',
]
})
export class TableNewEntryPageComponent implements OnInit {
  @Input() candidate?: Candidate;
  @Output() candidatesUpdated = new EventEmitter<Candidate[]>();

  technologies:string[];

  //addButtonClick = new EventEmitter<Candidate>();
  

  constructor(private candidateService: CandidateService) {
    this.technologies = [
      'C#',
      'Angular'
  ];
   }

   onSubmit(form: NgForm): void {
      form.resetForm();
      this.createCandidate(this.candidate)
   }

  ngOnInit(): void {

  }

  createCandidate(candidate: Candidate) {
    this.candidateService.createCandidate(candidate)
    .subscribe((candidates: Candidate[]) => this.candidatesUpdated.emit(candidates));
  }

  //onAddButtonClick(): void { 
  //  this.addButtonClick.emit(this.candidate);
  //  console.log(this.candidate);
  // }

}
