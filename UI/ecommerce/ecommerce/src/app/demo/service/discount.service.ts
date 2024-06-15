import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Discount } from '../api/discount';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class DiscountService {
    url = environment.urlBase;

    constructor(private http: HttpClient) {}

    getDiscount(): Observable<Discount[]> {
        return this.http.get<Discount[]>(`${this.url}/Discount/`);
    }

    getDiscountByID(id: number): Observable<Discount> {
        return this.http.get<Discount>(`${this.url}/Discount/${id}`);
    }

    postDiscount(Discount: Discount): Observable<Discount> {
        return this.http.post<Discount>(`${this.url}/Discount`, Discount);
    }

    deleteDiscount(id: number): Observable<Discount> {
        return this.http.delete<Discount>(`${this.url}/Discount/${id}`);
    }

    putDiscount(id: number, Discount: Discount): Observable<Discount> {
        return this.http.put<Discount>(`${this.url}/Discount/${id}`, Discount);
    }
}
