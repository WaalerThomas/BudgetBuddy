import { Location } from '@angular/common';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  private loggedIn$: BehaviorSubject<boolean>;

  constructor(
    private readonly router: Router,
    private readonly location: Location
  ) {
    this.loggedIn$ = new BehaviorSubject<boolean>(false);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn$.asObservable();
  }

  isLoggedInValue() {
    return this.loggedIn$.getValue();
  }
}
