import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map, Observable } from "rxjs";
import { IAuthInfo } from "../../models/authInfo.model";
import { ClientModel } from "../../modules/client/models/client.model";
import { ClientResourceService } from "../../resources/client/client-resource.service";

@Injectable({
    providedIn: 'root'
})
export class SecurityService {
    private loggedIn$: BehaviorSubject<boolean>;

    constructor(
        private readonly http: HttpClient,
        private readonly clientResourceService: ClientResourceService
    ) {
        this.loggedIn$ = new BehaviorSubject<boolean>(false);
    }

    login(credentials: { username: string, password: string, rememberMe: boolean }): Observable<any> {
        return new Observable((obs) => {
            const completeObs = (success: any) => {
                obs.next(success);
                obs.complete();
            };

            // TODO: Improve this
            this.clientResourceService.login(credentials).subscribe({
                next: (response: any) => {
                    if (response) {
                        this.setLoggedIn(true);
                        completeObs(response);
                    } else {
                        this.setLoggedIn(false);
                        completeObs(null);
                    }
                }
            })
        });
    }

    logOff(): void {
        if (this.isLoggedInValue() === false) {
            return;
        }

        this.clientResourceService.logOff().subscribe({
            next: (loggedOff: boolean) => {
                if (loggedOff) {
                    this.setLoggedIn(false);
                }
            }
        });
    }

    isLoggedIn(): Observable<boolean> {
        return this.loggedIn$.asObservable();
    }

    isLoggedInValue(): boolean {
        return this.loggedIn$.getValue();
    }

    private setLoggedIn(isLoggedIn: boolean): void {
        this.loggedIn$.next(isLoggedIn);
    }
}