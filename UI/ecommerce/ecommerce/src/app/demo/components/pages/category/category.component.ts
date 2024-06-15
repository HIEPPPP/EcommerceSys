import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/demo/api/product';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProductService } from 'src/app/demo/service/product.service';
import { Category } from 'src/app/demo/shared/category.model';
import { UploadEvent } from 'primeng/fileupload';
import { CategoryService } from 'src/app/demo/service/category.service';

@Component({
    templateUrl: './category.component.html',
    providers: [MessageService],
})
export class CategoryComponent implements OnInit {
    categoryDialog: boolean = false;

    deleteCategoryDialog: boolean = false;

    categories: Category[] = [];

    category: Category;

    selectedProducts: Category[] = [];

    submitted: boolean = false;

    constructor(
        private categoryService: CategoryService,
        private messageService: MessageService
    ) {}

    ngOnInit(): void {
        this.categoryService.getCategories().subscribe({
            next: (res) => {
                this.categories = res;
            },
            error: (err) => {
                console.log(err);
            },
        });

        this.category = {};
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    openNew() {
        this.category = {};
        this.submitted = false;
        this.categoryDialog = true;
    }

    hideDialog() {
        this.category = null;
        this.categoryDialog = false;
        this.submitted = false;
    }

    insertCategory() {
        this.categoryService.postCategory(this.category).subscribe({
            next: (res) => {
                // this.categories = [...this.categories, res];
                this.categoryService.getCategories().subscribe({
                    next: (res) => {
                        this.categories = res;
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.categoryDialog = false;
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Category Created',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Category Invalid',
                    life: 2000,
                });
            },
        });
    }

    editCategory(category: Category) {
        this.categoryDialog = true;
        this.category = category;
    }

    updateCategory(category: Category) {
        this.categoryService.updateCategory(category.id, category).subscribe({
            next: () => {
                this.categoryService.getCategories().subscribe({
                    next: (res) => {
                        this.categories = res;
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Category Updated',
                    life: 3000,
                });
            },
        });
    }

    saveCategory(category: Category) {
        const existCategory = this.categories.findIndex(
            (c) => c.id === category.id
        );
        if (existCategory < 0) {
            console.log('ADD Category');
            this.insertCategory();
        } else {
            console.log('UPDATE Category');
            this.updateCategory(category);
        }
        this.categoryDialog = false;
    }

    deleteCategory(id: number) {
        if (confirm('Are you sure to delete this category?')) {
            this.categoryService.deleteCategory(id).subscribe({
                next: (res) => {
                    this.categories = this.categories.filter((c) => {
                        return c.id !== res.id;
                    });
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'Category Deleted',
                        life: 3000,
                    });
                },
                error: (err) => {
                    console.log(err);

                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'Can not delete this category',
                        life: 3000,
                    });
                },
            });
        }
    }
}
