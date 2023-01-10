import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { AuthService } from '../../../shared/services/auth/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sign-in-google',
  templateUrl: './sign-in-google.component.html',
  styleUrls: ['./sign-in-google.component.css'],
})
export class SignInGoogleComponent implements OnInit {
  private clientId = environment.googleAuthClientId;

  constructor(
    private router: Router,
    private authService: AuthService,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {
    // @ts-ignore
    window.onload = () => {
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: this.clientId,
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,
      });
      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById('googleButton'),
        { theme: 'outline', size: 'large', width: '100%' }
      );
      // @ts-ignore
      google.accounts.id.prompt((_) => {});
    };
  }

  handleCredentialResponse(response: CredentialResponse) {
    this.authService.signIn(response.credential).subscribe({
      next: (_) => {
        this.ngZone.run(() => {
          this.router.navigate(['/home']);
        });
      },
      error: (error: any) => {
        console.log(error);
      },
    });
  }
}
