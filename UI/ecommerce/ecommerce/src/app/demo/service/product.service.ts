import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../api/product';
import { Observable } from 'rxjs';
import { Category } from '../shared/category.model';
import { Discount } from '../api/discount';
import { environment } from 'src/environments/environment';
@Injectable()
export class ProductService {
    url = environment.urlBase;
    product: Product;
    products: Product[] = [];
    constructor(private http: HttpClient) {}

    getProductsSmall() {
        return this.http
            .get<any>('assets/demo/data/products-small.json')
            .toPromise()
            .then((res) => res.data as Product[])
            .then((data) => data);
    }

    getProductsMixed() {
        return this.http
            .get<any>('assets/demo/data/products-mixed.json')
            .toPromise()
            .then((res) => res.data as Product[])
            .then((data) => data);
    }

    getProductsWithOrdersSmall() {
        return this.http
            .get<any>('assets/demo/data/products-orders-small.json')
            .toPromise()
            .then((res) => res.data as Product[])
            .then((data) => data);
    }

    // -----------------------------------------------------------------------

    getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(`${this.url}/product`);
    }

    // createProduct(product: Product, image: File) {
    //     const formData: FormData = new FormData();

    //     formData.append('name', product.name);
    //     formData.append('description', product.desc);
    //     formData.append('categoryId', product.categoryId.toString());
    //     formData.append('sku', product.sku);
    //     formData.append('discountId', product.discountId.toString());
    //     formData.append('quantity', product.quantity.toString());
    //     formData.append('price', product.price.toString());
    //     formData.append('image', image, image.name);

    //     return this.http.post(this.url, formData);
    // }

    createProduct(product: Product): Observable<Product> {
        return this.http.post<Product>(`${this.url}/product`, product);
    }

    deleteProduct(id: number): Observable<Product> {
        return this.http.delete<Product>(`${this.url}/product/${id}`);
    }

    updateProduct(id: Number, product: Product): Observable<Product> {
        return this.http.put<Product>(`${this.url}/product/${id}`, product);
    }

    // paginationPageSize() {
    //     return this.http.get(this.url + '/')
    // }

    getCate(): Observable<Category[]> {
        return this.http.get<Category[]>(`${this.url}/category`);
    }

    getDiscount(): Observable<Discount[]> {
        return this.http.get<Discount[]>(`${this.url}/discount`);
    }
}
