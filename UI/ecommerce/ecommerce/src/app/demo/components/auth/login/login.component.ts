import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../service/auth.service';
import { MessageService } from 'primeng/api';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
})
export class LoginComponent {
    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router
    ) {}

    loginForm = this.fb.group({
        username: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]],
    });

    get username() {
        return this.loginForm.controls['username'];
    }

    get password() {
        return this.loginForm.controls['password'];
    }

    loginUser() {
        let user = this.loginForm.value;
        let loggedData: any;
        this.authService.onLogin(user).subscribe({
            next: (res) => {
                if (res) {
                    alert('Login Success');
                    localStorage.setItem('data', JSON.stringify(res));
                    this.router.navigateByUrl('/dashboard');
                }
            },
            error: (err) => {
                alert('Username or password is wrong');
            },
        });
    }
}
