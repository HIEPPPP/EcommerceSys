import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../api/user';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    url = environment.urlBase;
    constructor(private http: HttpClient) {}

    getUsers(): Observable<User[]> {
        return this.http.get<User[]>(`${this.url}/user`);
    }

    postUser(user: User): Observable<User> {
        return this.http.post<User>(`${this.url}/user`, user);
    }

    deleteUser(id: number): Observable<User> {
        return this.http.delete<User>(`${this.url}/user/${id}`);
    }

    updateUser(id: number, user: User): Observable<User> {
        return this.http.put<User>(`${this.url}/user/${id}`, user);
    }
}
