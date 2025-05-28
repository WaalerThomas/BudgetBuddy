import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
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
        rememberme: new FormControl(false)
    });

    constructor(
        private router: Router,
        private securityService: SecurityService
    ) {
    }

    ngOnInit(): void {
    }

    hideModal() {
        // TODO: Send login credentials to some module/service that will handle logging on
        // TODO: Validate the form before proceeding with login
        
        this.securityService.login({
            username: this.loginForm.controls.username.value ?? '',
            password: this.loginForm.controls.password.value ?? '',
            rememberMe: this.loginForm.controls.rememberme.value ?? false
        }).subscribe({
            next: (response) => {
                console.log("Login successful", response);
                this.router.navigateByUrl('/');
            },
            error: (error) => {
                console.error("Login failed", error);
                // Handle login failure, e.g., show an error message
            }
        });

        //this.visible = false;
        console.log("Hiding the modal");
        console.log("Username: " + this.loginForm.controls.username.value);
        console.log("Password: " + this.loginForm.controls.password.value);
        console.log("Remember Me: " + this.loginForm.controls.rememberme.value);
    }
}