import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

export interface AuthData {
  encryptedToken: string;
  userEmail: string;
  userId: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _path = environment.apiUrl;

  user$ = new BehaviorSubject<User | null>(null);
  isAuthenticated!: boolean;

  constructor(private httpClient: HttpClient) {
    this.user$.subscribe((value) => (this.isAuthenticated = value != null));
  }

  signIn(credentials: string): Observable<AuthData> {
    const header = new HttpHeaders().set('Content-type', 'application/json');

    return this.httpClient
      .post<AuthData>(
        this._path + 'api/auth/signIn',
        JSON.stringify(credentials),
        { headers: header }
      )
      .pipe(
        tap((response: AuthData) => {
          localStorage.setItem('token', response.encryptedToken);
          this.user$.next(
            new User(
              response.userEmail,
              response.userId,
              response.encryptedToken
            )
          );
        })
      );
  }

  signOut = () => {
    localStorage.removeItem('token');
    this.user$.next(null);
  };
}
