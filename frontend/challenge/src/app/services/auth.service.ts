import { Injectable } from '@angular/core';
import { HttpClient, HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';


import { UserLogin } from '../models/UserLogin';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements HttpInterceptor {

  AUTH_URL: string = environment.apiUrl + '/Authenticate';

  constructor(
    private http: HttpClient
  ) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isAuthenticaded()) {
      const tokenReq: HttpRequest<any> = req.clone({
        setHeaders: {
          Authorization: `Bearer ${ localStorage.getItem('token') }`
        }
      });
      return next.handle(tokenReq);
    }
    return next.handle( req );
  }

  login(user: UserLogin): Observable<string> {
    return this.http.post(this.AUTH_URL, user, { responseType: 'text' })
      .pipe(
        retry(3)
      );
  }

  isAuthenticaded(): boolean {
    const tokenString = localStorage.getItem('token');
    if (tokenString != null) {
      return true;
    }
    return false;
  }
}
