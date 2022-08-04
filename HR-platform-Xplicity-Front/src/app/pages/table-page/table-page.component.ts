import { Component, OnInit } from '@angular/core';
import { Candidate } from '../../Models/candidate';
import { CandidateService } from '../../Services/candidate.service';

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css']
})
export class TablePageComponent implements OnInit {
  candidates: any[] = [];
  firstname: any;
  constructor(private candidateService: CandidateService ) { }

  ngOnInit(): void {
    this.candidateService.getCandidate()
    .subscribe(
      items => {
        this.candidates = items;
        console.log(this.candidates);
        this.firstname = items[0].firstName;
      });
  }

}
