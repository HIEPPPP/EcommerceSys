import { Product } from 'src/app/demo/api/product';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProductService } from 'src/app/demo/service/product.service';
import { Category } from 'src/app/demo/shared/category.model';
import { UploadEvent } from 'primeng/fileupload';
import { Discount } from 'src/app/demo/api/discount';

@Component({
    templateUrl: './crud.component.html',
    providers: [MessageService],
})
export class CrudComponent implements OnInit {
    productDialog: boolean = false;

    deleteProductDialog: boolean = false;

    deleteProductsDialog: boolean = false;

    products: Product[] = [];

    product: Product;

    selectedProducts: Product[] = [];

    submitted: boolean = false;

    cols: any[] = [];

    statuses: any[] = [];

    rowsPerPageOptions = [5, 10, 20];

    categories: Category[];

    discounts: Discount[];

    file: File;

    selectedFile: File = null;

    uploadedFiles: any[] = [];

    response: { filePath: '' };

    constructor(
        private productService: ProductService,
        private messageService: MessageService
    ) {}

    ngOnInit() {
        this.productService.getProducts().subscribe({
            next: (res) => {
                this.products = res as Product[];
            },
            error: (err) => {
                console.log(err);
            },
        });

        this.cols = [
            { field: 'code', header: 'Code' },
            { field: 'name', header: 'Name' },
            { field: 'image', header: 'Image' },
            { field: 'price', header: 'Price' },
            { field: 'category', header: 'Category' },
            { field: 'quantity', header: 'Quantity' },
            { field: 'discount', header: 'Discount' },
        ];

        this.product = {};

        this.productService.getCate().subscribe({
            next: (res) => {
                this.categories = res;
            },
            error: (err) => {
                console.log(err);
            },
        });

        this.productService.getDiscount().subscribe({
            next: (res) => {
                this.discounts = res;
            },
            error: (err) => {
                console.log(err);
            },
        });
    }

    uploadFinished = (event) => {
        this.response = event;
    };

    createImgPath(serverPath: string) {
        return `https://localhost:7088/${serverPath}`;
    }

    createId(): string {
        let id = '';
        const chars =
            'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        for (let i = 0; i < 5; i++) {
            id += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return id;
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    openNew() {
        this.product = {};
        this.submitted = false;
        this.productDialog = true;
    }

    deleteSelectedProducts() {
        this.deleteProductsDialog = true;
    }

    editProduct(product: Product) {
        this.product = { ...product };
        this.productDialog = true;
    }

    deleteProduct(id: number) {
        // this.deleteProductDialog = true;
        if (confirm('Are you sure to delete this product?'))
            this.productService.deleteProduct(id).subscribe({
                next: (res) => {
                    this.products = this.products.filter(
                        (p) => p.id !== res.id
                    );
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'Product Deleted',
                        life: 2000,
                    });
                },
            });
    }

    onFileSelected(event) {
        this.selectedFile = event.files[0];
        const reader = new FileReader();
        reader.onload = (e: any) => {
            this.product.image = e.target.result; // lưu hình ảnh dưới dạng base64 hoặc bạn có thể lưu đường dẫn URL sau khi tải lên
        };
        reader.readAsDataURL(this.selectedFile);
    }

    // confirmDelete() {
    //     this.deleteProductDialog = false;
    //     // this.products = this.products.filter(
    //     //     (val) => val.id !== this.product.id
    //     // );
    //     this.product = this.products.find((p) => {
    //         return p.id === this.product.id;
    //     });
    //     if (this.product) {
    //         this.productService.deleteProduct(this.product.id);
    //     }
    //     this.messageService.add({
    //         severity: 'success',
    //         summary: 'Successful',
    //         detail: 'Product Deleted',
    //         life: 3000,
    //     });
    //     this.product = {};
    // }

    // confirmDeleteSelected() {
    //     this.deleteProductsDialog = false;
    //     this.products = this.products.filter(
    //         (val) => !this.selectedProducts.includes(val)
    //     );
    //     this.messageService.add({
    //         severity: 'success',
    //         summary: 'Successful',
    //         detail: 'Products Deleted',
    //         life: 3000,
    //     });
    //     this.selectedProducts = [];
    // }

    hideDialog() {
        this.productDialog = false;
        this.submitted = false;
    }

    selectFile(e: Event) {
        this.file = e.target[0];
        console.log(this.file);
    }

    // onFileSelected(event) {
    //     this.selectedFile = event.files[0];
    // }

    insertProduct(product: Product) {
        product = {
            ...this.product,
            image: this.response.filePath,
        };
        console.log(product);

        this.productService.createProduct(product).subscribe({
            next: () => {
                this.productService.getProducts().subscribe({
                    next: (res) => {
                        this.products = res;
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Product Created',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not create this product!',
                    life: 2000,
                });
            },
        });
    }

    // uploadHandler(event) {
    //     this.onFileSelected(event);
    // }

    updateProduct(product: Product) {
        this.productService.updateProduct(product.id, product).subscribe({
            next: (res) => {
                // this.products = [...this.products, res];
                this.productService.getProducts().subscribe({
                    next: (res) => {
                        this.products = res as Product[];
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Product Updated',
                    life: 3000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Product Invalid',
                    life: 3000,
                });
            },
        });
    }

    saveProduct(product: Product) {
        this.submitted = true;
        const existId = this.products.findIndex((p) => p.id === product.id);
        if (existId < 0) {
            console.log('Add Product');
            this.insertProduct(product);
        } else {
            console.log('Update Product');
            this.updateProduct(product);
        }
        this.productDialog = false;
        this.product = {};
    }

    onUpload(event: UploadEvent) {
        console.log(event);
        this.messageService.add({
            severity: 'info',
            summary: 'Success',
            detail: 'File Uploaded with Basic Mode',
        });
    }
}
