import { filter } from 'rxjs';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { User } from 'src/app/demo/api/user';
import { UserService } from 'src/app/demo/service/user.service';
import { AddressComponent } from './address/address.component';
import { PaymentComponent } from './payment/payment.component';
import {
    FormBuilder,
    Validators,
    FormGroup,
    AbstractControl,
    ValidationErrors,
} from '@angular/forms';

@Component({
    templateUrl: './user.component.html',
    providers: [MessageService],
})
export class UserComponent implements OnInit {
    users: User[] = [];

    user: User;

    selectedUser: User[] = [];

    cols: any[] = [];

    userDialog: boolean = false;

    submitted: boolean = false;

    deleteUserDialog: boolean = false;

    userForm: FormGroup;

    constructor(
        private messageService: MessageService,
        private userService: UserService,
        private fb: FormBuilder
    ) {}

    @ViewChild(AddressComponent, { static: false })
    addressComponent!: AddressComponent;

    @ViewChild(PaymentComponent, { static: false })
    paymentComponent!: PaymentComponent;

    ngOnInit(): void {
        this.userService.getUsers().subscribe({
            next: (res) => {
                this.users = res;
            },
        });

        this.cols = [
            { field: '#', header: '#' },
            { field: 'id', header: 'ID' },
            { field: 'name', header: 'User Name' },
            { field: 'password', header: 'Password' },
            { field: 'firstName', header: 'First Name' },
            { field: 'lastName', header: 'Last Name' },
            { field: 'telephone', header: 'Telephone' },
        ];

        this.userForm = this.fb.group({
            id: [''],
            username: ['', [Validators.required]],
            password: ['', [Validators.required, Validators.minLength(7)]],
            firstName: ['', [Validators.required]],
            lastName: ['', [Validators.required]],
            telephone: [
                '',
                [Validators.required, this.vietnamesePhoneValidator],
            ],
        });
    }
    vietnamesePhoneValidator(
        control: AbstractControl
    ): ValidationErrors | null {
        const value = control.value;
        if (!value) {
            return null;
        }

        // Biểu thức chính quy cho số điện thoại Việt Nam
        const vietnamesePhoneRegex = /^(0|\+84)[3|5|7|8|9][0-9]{8}$/;

        const isValid = vietnamesePhoneRegex.test(value);
        return !isValid ? { invalidPhoneNumber: true } : null;
    }
    // passwordValidator(control: AbstractControl): ValidationErrors | null {
    //     const value = control.value;
    //     if (!value) {
    //         return null;
    //     }

    //     const hasUpperCase = /[A-Z]/.test(value);
    //     const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(value);

    //     const passwordValid = hasUpperCase && hasSpecialChar;
    //     return !passwordValid ? { passwordStrength: true } : null;
    // }

    get f() {
        return this.userForm.controls;
    }

    ngAfterViewInit() {
        if (!this.addressComponent) {
            console.error('AddressComponent not found');
        } else if (!this.paymentComponent) {
            console.error('PaymentComponent not found');
        }
    }

    callSelectedUserAddressDialog(user: User) {
        if (
            this.addressComponent &&
            typeof this.addressComponent.selectedUserDialog === 'function'
        ) {
            this.addressComponent.selectedUserDialog(user);
        } else {
            console.error(
                'selectedUserDialog is not a function or AddressComponent is not initialized'
            );
        }
    }

    callSelectedUserPaymentDialog(user: User) {
        if (
            this.paymentComponent &&
            typeof this.paymentComponent.selectedUserDialog === 'function'
        ) {
            this.paymentComponent.selectedUserDialog(user);
        } else {
            console.error(
                'selectedUserDialog is not a function or AddressComponent is not initialized'
            );
        }
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    openNew() {
        console.log(this.userForm);

        this.userForm.reset();
        this.submitted = false;
        this.userDialog = true;
    }

    hideDialog() {
        this.userDialog = false;
        this.submitted = false;
    }

    insertUser() {
        console.log(this.userForm);

        if (this.userForm.invalid) {
            this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: 'User can not create',
                life: 2000,
            });
            return;
        }
        this.userService.postUser(this.userForm.value).subscribe({
            next: (res) => {
                this.users = [...this.users, res];
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'User Created',
                    life: 2000,
                });
                this.userDialog = false;
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not create this user',
                    life: 2000,
                });
            },
        });
    }

    editUser(user: User) {
        // console.log(userForm);
        this.userForm.patchValue({
            id: user.id,
            username: user.username,
            password: user.password,
            firstName: user.firstName,
            lastName: user.lastName,
            telephone: user.telephone,
        });
        // this.userForm = userForm;
        this.userDialog = true;
    }

    updateUser() {
        console.log(this.userForm.value);
        if (this.userForm.invalid) {
            this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: 'User can not update',
                life: 2000,
            });
            return;
        }
        this.userService
            .updateUser(this.userForm.value.id, this.userForm.value)
            .subscribe({
                next: () => {
                    this.userService.getUsers().subscribe({
                        next: (res) => {
                            this.users = res;
                        },
                        error: (err) => {
                            console.log(err);
                        },
                    });
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'User Updated',
                        life: 2000,
                    });
                    this.userDialog = false;
                },
                error: (err) => {
                    console.log(err);
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'User can not update',
                        life: 2000,
                    });
                },
            });
    }

    saveProduct() {
        console.log(this.userForm);
        this.submitted = true;
        const existUser = this.users.findIndex(
            (u) => u.id === this.userForm.value.id
        );
        if (existUser < 0) {
            console.log('Add User');
            this.insertUser();
        } else {
            console.log('Update User');
            this.updateUser();
        }
    }

    deleteUser(id: number) {
        if (confirm('Are you sure to delete this user?')) {
            this.userService.deleteUser(id).subscribe({
                next: (res) => {
                    this.users = this.users.filter((u) => {
                        return u.id !== res.id;
                    });
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'User Deleted',
                        life: 2000,
                    });
                },
                error: (err) => {
                    console.log(err);
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'Can not delete this user',
                        life: 2000,
                    });
                },
            });
        }
    }

    // callSelectedUserDialog(user: User) {
    //     return this.addressComponent.selectedUserDialog(user);
    // }
}
