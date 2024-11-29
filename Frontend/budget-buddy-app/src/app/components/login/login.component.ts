import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';

import { SecurityService } from "../../services/security/security.service";

@Component({
    selector: 'login-component',
    standalone: true,
    imports: [ReactiveFormsModule, ButtonModule, DialogModule, InputTextModule, FloatLabelModule, PasswordModule, CheckboxModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
    visible: boolean = true;

    loginForm = new FormGroup({
        username: new FormControl(''),
        password: new FormControl(''),
    });

    constructor(
        private router: Router,
        private securityService: SecurityService
    ) {
    }

    ngOnInit(): void {
        this.handle();
    }

    private handle() {
        console.log("Security Login Value: " + this.securityService.isLoggedInValue());

        if (this.securityService.isLoggedInValue()) {
            this.navigateAfterLogin();
        } else {
        }
    }

    private navigateAfterLogin() {
        this.router.navigate(['']);
    }
}