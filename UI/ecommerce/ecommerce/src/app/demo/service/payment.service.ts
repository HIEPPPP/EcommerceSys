import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Payment } from '../api/payment';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class PaymentService {
    url = environment.urlBase;

    constructor(private http: HttpClient) {}

    getPayment(): Observable<Payment[]> {
        return this.http.get<Payment[]>(`${this.url}/UserPayment/`);
    }

    getPaymentByID(id: number): Observable<Payment> {
        return this.http.get<Payment>(`${this.url}/UserPayment/${id}`);
    }

    postPayment(Payment: Payment): Observable<Payment> {
        return this.http.post<Payment>(`${this.url}/UserPayment`, Payment);
    }

    deletePayment(id: number): Observable<Payment> {
        return this.http.delete<Payment>(`${this.url}/UserPayment/${id}`);
    }

    putPayment(id: number, Payment: Payment): Observable<Payment> {
        return this.http.put<Payment>(`${this.url}/UserPayment/${id}`, Payment);
    }
}
