import { Component, OnInit } from '@angular/core';
import { Candidate } from '../../Models/candidate';
import { CandidateService } from '../../Services/candidate.service';

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css']
})
export class TablePageComponent implements OnInit {
  candidates: Candidate[] = [];
  constructor(private candidateService: CandidateService ) { }

  ngOnInit(): void {
    this.candidates = this.candidateService.getCandidate();
    console.log(this.candidates);
  }

}
