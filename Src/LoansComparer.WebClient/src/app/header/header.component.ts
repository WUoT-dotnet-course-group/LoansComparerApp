import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isAuthenticated!: boolean;
  userSignedIn$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor() {}

  ngOnInit(): void {
    this.userSignedIn$.subscribe(
      (signedIn: boolean) => (this.isAuthenticated = signedIn)
    );
  }

  onSignIn(): void {}

  onRegister(): void {}
}
