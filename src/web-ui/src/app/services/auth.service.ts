import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private tokenKey = 'token';
  private authState = new BehaviorSubject<boolean>(this.hasToken());
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  private hasToken(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/api/token`, { username, password })
      .pipe(tap(res => {
        localStorage.setItem(this.tokenKey, res.token);
        this.authState.next(true);
      }));
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.authState.next(false);
  }

  isAuthenticated() {
    return this.authState.asObservable();
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}
