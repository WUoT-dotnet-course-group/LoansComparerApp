import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from '../shared/services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  onSignIn(): void {}

  get isAuthenticated() {
    return this.authService.isAuthenticated;
  }
}
