import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NonNullableFormBuilder } from '@angular/forms';
import { RouterLink, RouterOutlet } from '@angular/router';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'budget-buddy-app';

  constructor(private http: HttpClient) {
    // Called first before the ngOnInit()
  }

  ngOnInit(): void {
    this.hello();
  }

  hello() {
    // TODO: Figure out how to check if we are logged in or not

    this.http.get("/api/account").subscribe(x => {
      console.log(x);
    });
    
  }
}
