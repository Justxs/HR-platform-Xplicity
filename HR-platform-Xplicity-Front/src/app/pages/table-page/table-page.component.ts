import { Technology } from '../../Models/technology';
import { Component, OnInit } from '@angular/core';
import { Candidate } from '../../Models/candidate';
import { CandidateService } from '../../Services/candidate.service';
import { TechnologyService } from '../../Services/technology.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table-page',
  templateUrl: './table-page.component.html',
  styleUrls: ['./table-page.component.css']
})
export class TablePageComponent implements OnInit {
  candidates: Candidate[] = [];
  technologies: Technology[] = [];
  candidateDialog: boolean = false;
  submitted: boolean = false;

  constructor(private router: Router, private candidateService: CandidateService, private technologyService: TechnologyService) { }

  ngOnInit(): void {
    this.candidateService.getCandidate()
      .subscribe(
        items => {
          this.candidates = items;
        });

    this.technologyService.getTechnologies()
      .subscribe(
        items => {
          this.technologies = items;
        });
        
  }
  openNew() {
    this.submitted = false;
    this.candidateDialog = true;
}
//added a logout button for table page
  logOut(){
    localStorage.removeItem("Jwt");
    this.router.navigate([""])
  }

}
