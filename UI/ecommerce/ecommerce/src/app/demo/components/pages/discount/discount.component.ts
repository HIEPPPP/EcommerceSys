import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Discount } from 'src/app/demo/api/discount';
import { DiscountService } from 'src/app/demo/service/discount.service';

@Component({
    templateUrl: './discount.component.html',
    providers: [MessageService],
})
export class DiscountComponent implements OnInit {
    discountDialog: boolean = false;

    discounts: Discount[] = [];

    discount: Discount;

    cols: any[] = [];

    submitted: boolean = false;

    stateOptions: any[] = [];

    selectedDiscounts: Discount[] = [];

    checked: boolean = false;

    constructor(
        private messageService: MessageService,
        private discountService: DiscountService
    ) {}

    ngOnInit(): void {
        this.cols = [
            { field: '#', header: '#' },
            { field: 'id', header: 'Id' },
            { field: 'name', header: 'Name' },
            { field: 'desc', header: 'Description' },
            { field: 'discountPercent', header: 'Discount Percent' },
            { field: 'active', header: 'Active' },
        ];

        this.stateOptions = [
            { label: 'Off', value: false },
            { label: 'On', value: true },
        ];

        this.discountService.getDiscount().subscribe({
            next: (res) => {
                this.discounts = res;
            },
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    openNew() {
        this.discountDialog = true;
        this.discount = {};
    }

    hideDialog() {
        this.discountDialog = false;
        this.submitted = false;
    }

    updateDiscountDialog(discount: Discount) {
        this.discount = { ...discount };
        this.discountDialog = true;
    }

    insertDiscount(discount: Discount) {
        this.discountService.postDiscount(discount).subscribe({
            next: (res) => {
                this.discountService.getDiscount().subscribe({
                    next: (res) => {
                        this.discounts = res;
                    },
                });
                // this.discounts = [...this.discounts, res];
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Discount Created',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not create this discount!',
                    life: 2000,
                });
            },
        });
    }

    editDiscount(discount: Discount) {
        console.log(discount);

        this.discountService.putDiscount(discount.id, discount).subscribe({
            next: () => {
                this.discountService.getDiscount().subscribe({
                    next: (res) => {
                        this.discounts = res;
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Discount Created',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not update this discount!',
                    life: 2000,
                });
            },
        });
    }

    saveDiscount(discount: Discount) {
        const existId = this.discounts.findIndex((d) => d.id === discount.id);
        if (existId < 0) {
            console.log('Add Discount');
            this.insertDiscount(discount);
        } else {
            console.log('Update Discount');
            this.editDiscount(discount);
        }
        this.discountDialog = false;
    }

    deleteDiscount(id: number) {
        this.discountService.deleteDiscount(id).subscribe({
            next: (res) => {
                if (confirm('Are you sure want delete this discount?')) {
                    this.discounts = this.discounts.filter(
                        (d) => d.id !== res.id
                    );
                }
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Discount Deleted',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not delete this discount!',
                    life: 2000,
                });
            },
        });
    }
}
