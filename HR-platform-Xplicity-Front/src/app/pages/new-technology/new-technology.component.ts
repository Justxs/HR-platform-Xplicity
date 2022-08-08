import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Technology } from 'src/app/Models/technology';
import { TechnologyService } from 'src/app/Services/technology.service';

@Component({
  selector: 'app-new-technology',
  templateUrl: './new-technology.component.html',
  styleUrls: ['./new-technology.component.css']
})
export class NewTechnologyComponent implements OnInit {
  @Input() newTechnology: Technology = new Technology;
  @Output() technologiesUpdated = new EventEmitter<Technology[]>();

  constructor(private technologyService: TechnologyService) {
    
  }

  onSubmit(): void {
    //form.resetForm();
  }

  ngOnInit(): void { }

  createTechnology(newTechnology: Technology) {
    this.technologyService.createTechnology(newTechnology)
      .subscribe((technologies: Technology[]) => {
        return this.technologiesUpdated.emit(technologies);
      });
  }

}
