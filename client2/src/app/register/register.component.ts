import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input,Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  model:any={}
  @Input() usersFromHomeComponent:any;
  @Output() userEventEmit=new EventEmitter();

  ngOnInit(): void {
    
  }

  register()
  {
    console.log(this.model);
  }

  cancel()
  {
    this.userEventEmit.emit(false);
  }
}
