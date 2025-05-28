import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { ClientResourceService } from '../../resources/client/client-resource.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MenubarModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  items: MenuItem;

  constructor(
    private readonly clientResourceService: ClientResourceService
  ) {}

  logout() {
    this.clientResourceService.logOff().subscribe({
      next: (loggedOff: boolean) => {
        if (loggedOff) {
          console.log('Logged off successfully');
          // Optionally, redirect to login page or home page
        } else {
          console.error('Failed to log off');
        }
      },
      error: (error) => {
        console.error('Error during log off:', error);
      }
    });
  }
}
