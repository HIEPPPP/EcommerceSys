import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
    const router = inject(Router);

    let localData = localStorage.getItem('data');
    if (localData != null) {
        return true;
    } else {
        router.navigateByUrl('/login');
        return false;
    }
};
