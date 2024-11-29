import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SecurityService } from './security.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService {
    constructor(
        private readonly router: Router,
        private readonly securityService: SecurityService)
    {
    }

    canActivate(): Observable<boolean> {
        return new Observable((observer$) => {
            this.securityService
                .isLoggedIn()
                .subscribe((loggedIn) => {
                    const localLoginUrl = '/login';
                    this.router.navigateByUrl(localLoginUrl);
                })
                .unsubscribe();

            observer$.next(true);
        });
    }
}
