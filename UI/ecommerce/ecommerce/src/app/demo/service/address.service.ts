import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Address } from '../api/address';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class AddressService {
    url = environment.urlBase;
    constructor(private http: HttpClient) {}

    getAddress(): Observable<Address[]> {
        return this.http.get<Address[]>(`${this.url}/UserAddress/`);
    }

    getAddressByID(id: number): Observable<Address> {
        return this.http.get<Address>(`${this.url}/UserAddress/${id}`);
    }

    postAddress(address: Address): Observable<Address> {
        return this.http.post<Address>(`${this.url}/UserAddress`, address);
    }

    deleteAddress(id: number): Observable<Address> {
        return this.http.delete<Address>(`${this.url}/UserAddress/${id}`);
    }

    putAddress(id: number, address: Address): Observable<Address> {
        return this.http.put<Address>(`${this.url}/UserAddress/${id}`, address);
    }
}
