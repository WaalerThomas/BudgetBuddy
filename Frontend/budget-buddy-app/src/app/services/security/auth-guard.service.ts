import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { IAuthInfo } from '../../models/authInfo.model';
import { SecurityService } from './security.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService {
    constructor(
        private readonly router: Router,
        private readonly securityService: SecurityService,
    )
    {
    }

    canActivate(): Observable<boolean> {
        return new Observable((observer$) => {
            this.securityService.isLoggedIn()
                .subscribe((isLoggedIn: boolean) => {
                    if (!isLoggedIn) {
                        const localLoginUrl = '/login';
                        this.router.navigateByUrl(localLoginUrl);
                    }
                })
                .unsubscribe();
            observer$.next(true);
        });
    }
}
