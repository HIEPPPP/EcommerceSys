import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Payment } from 'src/app/demo/api/payment';
import { User } from 'src/app/demo/api/user';
import { PaymentService } from 'src/app/demo/service/payment.service';

@Component({
    selector: 'app-payment',
    templateUrl: './payment.component.html',
})
export class PaymentComponent implements OnInit {
    paymentDialog: boolean = false;

    newPaymentDialog: boolean = false;

    userSelected: User;

    payments: Payment[] = [];

    payment: Payment;

    columnsPayment: any[] = [];

    paymentType: any[] = [];

    constructor(
        private messageService: MessageService,
        private paymentService: PaymentService
    ) {}

    ngOnInit(): void {
        this.columnsPayment = [
            { field: '#', header: '#' },
            { field: 'id', header: 'ID' },
            { field: 'userId', header: 'User ID' },
            { field: 'paymentType', header: 'Payment Type' },
            { field: 'provider', header: 'Provider' },
            { field: 'accountNo', header: 'Account Number' },
            { field: 'expiry', header: 'Expiry' },
        ];

        this.paymentType = [{ name: 'Bank Transfer' }, { name: 'Debit Cards' }];
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    selectedUserDialog(user: User) {
        this.paymentDialog = true;
        this.userSelected = user;
        this.getPayment(user.id);
    }

    createPaymentDialog() {
        this.newPaymentDialog = true;
        this.payment = { userId: this.userSelected.id };
    }

    getPayment(id: number) {
        this.paymentService.getPayment().subscribe({
            next: (res) => {
                this.payments = res.filter((payment) => {
                    return payment.userId === id;
                });
            },
        });
    }

    createPayment() {
        this.newPaymentDialog = true;
        this.payment.userId = this.userSelected.id;
    }

    insertPayment(payment: Payment) {
        this.paymentService.postPayment(payment).subscribe({
            next: () => {
                // this.payments = [...this.payments, res];
                this.paymentService.getPayment().subscribe({
                    next: (res) => {
                        this.payments = res.filter(
                            (p) => p.userId === payment.userId
                        );
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Success',
                    detail: 'Payment Created',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not create this payment',
                    life: 2000,
                });
            },
        });
    }

    editPayment(payment: Payment) {
        this.newPaymentDialog = true;
        this.payment = payment;
        this.paymentService.putPayment(payment.id, payment).subscribe({
            next: () => {
                this.paymentService.getPayment().subscribe({
                    next: (res) => {
                        this.payments = res.filter(
                            (p) => p.userId === payment.userId
                        );
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Success',
                    detail: 'Payment Update',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not update this payment!',
                    life: 2000,
                });
            },
        });
    }

    savePayment(payment: Payment) {
        const existPayment = this.payments.findIndex((d) => {
            return d.id === payment.id;
        });
        console.log(existPayment);

        if (existPayment < 0) {
            console.log('Add payment');
            this.insertPayment(payment);
        } else {
            console.log('Update payment');
            this.editPayment(payment);
        }
        this.newPaymentDialog = false;
    }

    deletePayment(id: number) {
        this.paymentService.deletePayment(id).subscribe({
            next: (res) => {
                if (confirm('Are you sure want delete this payment?')) {
                    this.payments = this.payments.filter((d) => {
                        return d.id !== res.id;
                    });
                }
                this.messageService.add({
                    severity: 'success',
                    summary: 'Success',
                    detail: 'Payment Deleted',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not delete this payment',
                    life: 2000,
                });
            },
        });
    }

    hidePaymentDialog() {
        this.newPaymentDialog = false;
    }
}
