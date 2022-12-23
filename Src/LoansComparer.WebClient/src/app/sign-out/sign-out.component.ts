import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth/auth.service';

@Component({
  selector: 'app-sign-out',
  templateUrl: './sign-out.component.html',
  styleUrls: ['./sign-out.component.css'],
})
export class SignOutComponent implements OnInit {
  constructor(
    private router: Router,
    private service: AuthService,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {}

  logout() {
    this.service.signOutExternal();
    this.ngZone.run(() => {
      this.router.navigate(['/home']).then(() => window.location.reload());
    });
  }
}
