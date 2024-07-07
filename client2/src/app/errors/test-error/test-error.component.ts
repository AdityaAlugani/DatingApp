import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css'
})
export class TestErrorComponent {


  baseUrl="http://localhost:5000/api/buggy";

  Mismatched:string[]=[];

  constructor(private HttpService:HttpClient){} 
Notfound() {
  this.HttpService.get(this.baseUrl+"/not-found").subscribe({
    next:(first)=>console.log("first",first),
    error:(error)=>console.log("error",error)
})
}
InternalError() {
  this.HttpService.get(this.baseUrl+"/server-error").subscribe({
    next:(first)=>console.log("first",first),
    error:(error)=>console.log("error",error)
})
}
Unauthorized() {
  this.HttpService.get(this.baseUrl+"/auth",{responseType:"text"}).subscribe({
    next:(first)=>console.log("first",first),
    error:(error)=>console.log("error",error)
})
}
Badrequest() {
  this.HttpService.get(this.baseUrl+"/bad-request").subscribe({
    next:(first)=>console.log("first",first),
    error:(error)=>console.log("error",error)
})
}

Validation() {
  this.HttpService.post("http://localhost:5000/api/account/register",{}).subscribe({
    next:(first)=>console.log("first",first),
    error:(error)=>
    {
      this.Mismatched=error;
      console.log("error",error);
    }
})
  }

}
