import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-table-new-entry-page',
  templateUrl: './table-new-entry-page.component.html',
  styleUrls: ['./table-new-entry-page.component.css',
]
})
export class TableNewEntryPageComponent implements OnInit {



  date1?: Date;
  available: boolean = false;
  technologies:string[];
  selectedTechnologies?: any[];

  constructor() {
    this.technologies = [
      'C#',
      'Angular'
  ];
   }

  ngOnInit(): void {
  }

}
