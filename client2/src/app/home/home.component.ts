import { HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  registermode=false;
  users:any;

  constructor(private httpService:HttpClient){}

  ngOnInit()
  {

  }

  toggleregister()
  {
    this.registermode=!this.registermode;
  }

  changeregister($event: boolean) 
  {
    this.registermode=$event;
  }
}
