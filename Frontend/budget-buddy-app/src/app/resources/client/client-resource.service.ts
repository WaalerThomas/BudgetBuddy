import { Injectable } from "@angular/core";
import { Observable, ReplaySubject } from "rxjs";
import { ClientModel } from "../../modules/client/models/client.model";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class ClientResourceService {
    client$: ReplaySubject<ClientModel>;

    private readonly clientUrl = '/api/client'

    constructor(
        private readonly http: HttpClient
    ) {
        this.client$ = new ReplaySubject<ClientModel>(1);
    }

    login(credentials: { username: string, password: string, rememberMe: boolean }) {
        const url = `${this.clientUrl}/login`;
        return this.http.post(url, credentials);
    }

    logOff(): Observable<boolean> {
        const url = `${this.clientUrl}/logout`;
        return this.http.post<boolean>(url, {});
    }

    getMe(): Observable<ClientModel> {
        const url = `${this.clientUrl}/me`;
        return this.http.get<ClientModel>(url);
    }
}