import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuardService } from './services/security/auth-guard.service';
import { NoPageComponent } from './components/no-page/no-page.component';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent,
        title: 'Budget Buddy - Login'
    },
    {
        path: '',
        component: DashboardComponent,
        title: 'Budget Buddy',
        canActivate: [AuthGuardService]
    },
    {
        path: '**',
        component: NoPageComponent
    }
];
