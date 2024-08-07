import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {
  error:any=null;

  constructor(private router:Router)
  {
    const navigation=router.getCurrentNavigation();
    this.error=navigation?.extras?.state?.['error'];
    console.log(this.error);
  }
}
