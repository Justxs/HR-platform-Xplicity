import { Technology } from './../../Models/technologies';
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
  technologies: Technology[] = [];
  constructor(private candidateService: CandidateService ) { }

  ngOnInit(): void {
    this.candidateService.getCandidate()
    .subscribe(
      items => {
        this.candidates = items;
      });
    this.technologies = [
      {tittle: "C#"},
      {tittle: "C"},
      {tittle: "C++"}
    ]
  }

}
