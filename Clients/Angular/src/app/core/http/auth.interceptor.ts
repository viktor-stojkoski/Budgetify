import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isAuthenticated()) {
      let token = '';

      this.authService
        .getToken()
        .then((x) => {
          console.log(x);
          token = x.idToken;
        })
        .catch((x) => console.error(x))
        .then((x) => {
          const authRequest = req.clone({
            setHeaders: {
              'Content-Type': 'application/json',
              Authorization: `Bearer ${token}`,
              Accept: 'application/json'
            }
          });

          return next.handle(authRequest);
        });

      return next.handle(req);
      // console.log(token);
      // const authRequest = req.clone({
      //   setHeaders: {
      //     'Content-Type': 'application/json',
      //     Authorization: token,
      //     Accept: 'application/json'
      //   }
      // });

      // return next.handle(authRequest);
    } else {
      return next.handle(req);
    }
  }
}
