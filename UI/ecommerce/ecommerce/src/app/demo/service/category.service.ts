import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../shared/category.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class CategoryService {
    url = environment.urlBase;

    constructor(private http: HttpClient) {}

    getCategories(): Observable<Category[]> {
        return this.http.get<Category[]>(`${this.url}/category`);
    }

    postCategory(category: Category): Observable<Category> {
        return this.http.post<Category>(`${this.url}/category`, category);
    }

    deleteCategory(id: number): Observable<Category> {
        return this.http.delete<Category>(`${this.url}/Category/${id}`);
    }

    updateCategory(id: number, category: Category): Observable<Category> {
        return this.http.put<Category>(`${this.url}/category/${id}`, category);
    }
}
