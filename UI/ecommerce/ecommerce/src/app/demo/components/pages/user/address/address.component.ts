import { Component, OnInit } from '@angular/core';
import {
    AbstractControl,
    FormBuilder,
    FormGroup,
    ValidationErrors,
    Validators,
} from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Address } from 'src/app/demo/api/address';
import { User } from 'src/app/demo/api/user';
import { AddressService } from 'src/app/demo/service/address.service';

@Component({
    selector: 'app-address',
    templateUrl: './address.component.html',
    providers: [MessageService],
})
export class AddressComponent implements OnInit {
    // Address Dialog
    addressDialog: boolean = false;

    newAddressDialog: boolean = false;

    userSelected: any = null;

    addresses: Address[] = [];

    address: Address;

    columnsAddress: any[] = [];

    addressForm: FormGroup;

    constructor(
        private messageService: MessageService,
        private addressService: AddressService,
        private fb: FormBuilder
    ) {}

    ngOnInit(): void {
        this.columnsAddress = [
            { field: '#', header: '#' },
            { field: 'id', header: 'ID' },
            { field: 'userId', header: 'User ID' },
            { field: 'addressLine1', header: 'Address 1' },
            { field: 'addressLine2', header: 'Address 2' },
            { field: 'city', header: 'City' },
            { field: 'postalCode', header: 'Postal Code' },
            { field: 'country', header: 'Country' },
            { field: 'telephone', header: 'Telephone' },
            { field: 'mobile', header: 'Mobile' },
        ];

        this.addressForm = this.fb.group({
            id: [''],
            userId: [{ value: '', readonly: true }],
            addressLine1: ['', [Validators.required]],
            addressLine2: [''],
            city: ['', [Validators.required]],
            postalCode: ['', [Validators.required]],
            country: ['', [Validators.required]],
            telephone: [
                '',
                [Validators.required, this.vietnamesePhoneValidator],
            ],
            mobile: ['', [this.vietnamesePhoneValidator]],
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

    get f() {
        return this.addressForm.controls;
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    selectedUserDialog(user: User) {
        this.addressDialog = true;
        this.userSelected = user;
        this.getAddress(user.id);
    }

    createAddressDialog() {
        this.addressForm.reset();
        if (this.userSelected) {
            this.newAddressDialog = true;
            this.addressForm.patchValue({
                userId: this.userSelected.id,
            });
        }
    }

    getAddress(id: number) {
        this.addressService.getAddress().subscribe({
            next: (res) => {
                this.addresses = res.filter((address) => {
                    return address.userId === id;
                });
            },
        });
    }

    updateAddressDialog(address: Address) {
        this.newAddressDialog = true;
        this.addressForm.patchValue({
            id: address.id,
            userId: this.userSelected.id,
            addressLine1: address.addressLine1,
            addressLine2: address.addressLine2,
            city: address.city,
            postalCode: address.postalCode,
            country: address.country,
            telephone: address.telephone,
            mobile: address.mobile,
        });
    }

    insertAddress() {
        console.log(this.addressForm.value);

        this.addressService.postAddress(this.addressForm.value).subscribe({
            next: () => {
                // this.addresses = [...this.addresses, res];
                this.addressService.getAddress().subscribe({
                    next: (res) => {
                        this.addresses = res.filter(
                            (d) => d.userId == this.addressForm.value.userId
                        );
                        console.log(this.addresses);
                    },
                    error: (err) => {
                        console.log(err);
                    },
                });
                this.messageService.add({
                    severity: 'success',
                    summary: 'Success',
                    detail: 'Address Created',
                    life: 2000,
                });
            },
            error: (err) => {
                console.log(err);
            },
        });
    }

    editAddress() {
        this.addressService
            .putAddress(this.addressForm.value.id, this.addressForm.value)
            .subscribe({
                next: () => {
                    this.addressService.getAddress().subscribe({
                        next: (res) => {
                            this.addresses = res.filter(
                                (d) => d.userId == this.addressForm.value.userId
                            );
                        },
                        error: (err) => {
                            console.log(err);
                        },
                    });
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Success',
                        detail: 'Address Updated',
                        life: 2000,
                    });
                },
                error: (err) => {
                    console.log(err);
                    this.messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: 'Can not update this address!',
                        life: 2000,
                    });
                },
            });
    }

    saveAddress() {
        const existAddress = this.addresses.findIndex(
            (d) => d.id === this.addressForm.value.id
        );
        if (existAddress < 0) {
            console.log('Add Address');
            this.insertAddress();
        } else {
            console.log('Update Address');
            this.editAddress();
        }
        this.newAddressDialog = false;
    }

    deleteAddress(id: number) {
        this.addressService.deleteAddress(id).subscribe({
            next: (res) => {
                if (confirm('Are you sure want delete this address?')) {
                    this.addresses = this.addresses.filter(
                        (d) => d.id !== res.id
                    );
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Success',
                        detail: 'Address Deleted',
                        life: 2000,
                    });
                }
            },
            error: (err) => {
                console.log(err);
                this.messageService.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Can not delete this address',
                    life: 2000,
                });
            },
        });
    }

    hideAddressDialog() {
        this.newAddressDialog = false;
    }
}
