import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../api/user';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    public $refreshToken = new Subject<boolean>();

    url = environment.urlBase;

    constructor(private http: HttpClient) {
        this.$refreshToken.subscribe({
            next: (res) => {
                this.getRefreshToken();
            },
        });
    }

    onLogin(user: any) {
        return this.http.post(`${this.url}/Auth/Login`, user);
    }

    getRefreshToken() {
        let loggedUserData: any;
        const localData = localStorage.getItem('data');
        if (localData != null) {
            loggedUserData = JSON.parse(localData);
        }

        if (loggedUserData != null) {
            const obj = {
                jwtToken: loggedUserData.jwtToken,
                refreshToken: loggedUserData.refreshToken,
            };

            this.http.post(`${this.url}/Auth/RefreshToken`, obj).subscribe({
                next: (res) => {
                    if (res) {
                        localStorage.setItem('data', JSON.stringify(res));
                    }
                },
            });
        }
    }
}
