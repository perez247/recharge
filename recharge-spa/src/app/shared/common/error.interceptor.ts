import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

 @Injectable()

 export class ErrorInterceptor implements HttpInterceptor {
     intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //  next.handle(req).subscribe(x => console.log(x));
         return next.handle(req).pipe(
             catchError((error) => {
                 if (error instanceof HttpErrorResponse) {
                    if (error.status === 401) {
                        console.log(error.statusText);
                        return throwError(error.statusText.toLowerCase());
                    }

                    const applicationError = error.headers.get('Application-Error');
                    if (applicationError) {
                        console.log(applicationError);
                         return throwError(applicationError.toUpperCase);
                    }

                    const serverError = error.error;
                    let modelStateEror = '';
                    let errorMessage: string;

                    if (serverError && typeof serverError === 'object') {
                        for (const key in serverError) {
                            if (serverError[key]) {

                                if (typeof serverError[key] === 'object') {
                                    // console.log(Object.values(serverError[key]));
                                    modelStateEror += Object.values(serverError[key])[1] + '\n';
                                    continue;
                                }
                                errorMessage = serverError[key] + '';
                                modelStateEror += errorMessage.toLowerCase() + '\n';
                            }
                        }
                    }
                    // console.log(serverError);
                    return throwError(modelStateEror || serverError || 'Server Error');
                 }
             } )
         );
     }

    //  private loopError(serverError) {
    //     for (const key in serverError) {
    //         if (serverError[key]) {
    //             const errorMessge: string = serverError[key] + '';
    //             modelStateEror += errorMessge.toLowerCase() + '\n';
    //         }
    //     }
    //  }

 }


export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
