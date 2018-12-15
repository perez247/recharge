import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import * as _ from 'lodash';
import { Router } from '@angular/router';

 @Injectable()

 export class ErrorInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService, private router: Router) {}

     intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //  next.handle(req).subscribe(x => console.log(x));
         return next.handle(req).pipe(
             catchError((error) => {
                 if (error instanceof HttpErrorResponse) {
                    if (error.status === 401) {
                        this.authService.logout();
                        this.router.navigate(['auth']);
                        return throwError(error.statusText.toLowerCase());
                    }

                    const applicationError = error.headers.get('Application-Error');
                    if (applicationError) {
                        console.log(applicationError);
                         return throwError(applicationError.toUpperCase);
                    }

                    const serverError = error.error;
                    let modelStateEror = '';

                    if (typeof serverError === 'object') {

                        _.values(serverError).forEach((element) => {
                            if (element && typeof element === 'object') {
                                _.values(element).forEach((ele) => {
                                    modelStateEror += ele + '\n';
                                });
                            } else {
                                modelStateEror += element + '\n';
                            }
                        });
                    }

                    const finalError = modelStateEror || serverError || 'Server Error';
                    return throwError(finalError);
                 }
             } )
         );
     }

 }


export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
