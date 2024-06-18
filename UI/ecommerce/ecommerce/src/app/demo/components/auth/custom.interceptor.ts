import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../../service/auth.service';

export const customInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    let loggedUserData: any;
    const localData = localStorage.getItem('data');
    if (localData != null) {
        loggedUserData = JSON.parse(localData);
    }
    if (loggedUserData != null) {
        const cloneRequest = req.clone({
            setHeaders: {
                Authorization: `Bearer ${loggedUserData.jwtToken}`,
            },
        });
        return next(cloneRequest).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    // const isRefresh = confirm('Refresh Token nhe?');
                    // if (isRefresh) {
                    // }
                    authService.$refreshToken.next(true);
                }
                return throwError(() => error);
            })
        );
    }
    return next(req);
};
