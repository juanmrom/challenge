import { Injectable } from '@angular/core';
import { Observable, EMPTY } from 'rxjs';
import { catchError, retry, shareReplay } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import * as jwt_decode from "jwt-decode";

import { UserLogin } from '../models/model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  AUTH_URL: string = environment.apiUrl + '/Authenticate';

  constructor(
    private http: HttpClient
  ) { }

  login(user: UserLogin): Observable<string> {
    return this.http.post(this.AUTH_URL, user, { responseType: 'text' });
  }

  isAuthenticaded(): boolean {
    const tokenString = localStorage.getItem('token');
    if (tokenString != null) {
      return true;
    }
    return false;
  }
}
