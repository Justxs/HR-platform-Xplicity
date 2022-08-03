import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Candidate } from 'src/app/Models/candidate';

@Component({
  selector: 'app-table-new-entry-page',
  templateUrl: './table-new-entry-page.component.html',
  styleUrls: ['./table-new-entry-page.component.css',
]
})
export class TableNewEntryPageComponent implements OnInit {

  technologies:string[];

  addButtonClick = new EventEmitter<Candidate>();
  candidate: Candidate = new Candidate();
  

  constructor() {
    this.technologies = [
      'C#',
      'Angular'
  ];
   }

   onSubmit(form: NgForm): void {
      form.resetForm();
   }

  ngOnInit(): void {

  }

  onAddButtonClick(): void { 
    this.addButtonClick.emit(this.candidate);
    console.log(this.candidate);

   }

}
