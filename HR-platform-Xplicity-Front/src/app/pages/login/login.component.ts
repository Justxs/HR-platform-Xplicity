import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserServiceService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Output()
  addButtonClick = new EventEmitter<User>();

  user = new User();
  constructor() {}

  ngOnInit(): void {
  }
  
  onAddButtonClick(): void {
    this.addButtonClick.emit(this.user);
  }

}
