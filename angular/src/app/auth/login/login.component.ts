import { NotificationService } from './../../shared/Services/notification.service';
import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ACCESS_TOKEN, REFRESH_TOKEN } from 'src/app/shared/Constants/keys.constant';
import { LoginRequestDto } from 'src/app/shared/Models/login-request.dto';
import { LoginResponseDto } from 'src/app/shared/Models/login-response.dto';
import { AuthService } from 'src/app/shared/Services/auth.service';
import { TokenStorageService } from 'src/app/shared/Services/token.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .p-password input {
            width: 100%;
            padding:1rem;
        }

        :host ::ng-deep .pi-eye{
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }

        :host ::ng-deep .pi-eye-slash{
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent implements OnDestroy {
    private ngUnsubscribe = new Subject<void>();
    valCheck: string[] = ['remember'];
    blockedPanel = false;

    password!: string;

    loginForm : FormGroup;

    constructor(
        public layoutService: LayoutService, 
        private fb : FormBuilder, 
        private authService : AuthService,
        private router: Router,
        private tokenService : TokenStorageService,
        private notificationService : NotificationService
    ) { 
        this.loginForm = fb.group({
            username : new FormControl('', Validators.required),
            password : new FormControl('', Validators.required)
        })
    }

    public login() : void 
    {
        this.toggleBlockUI(true);
        var request: LoginRequestDto = {
            username: this.loginForm.controls['username'].value,
            password: this.loginForm.controls['password'].value,
        }

        this.authService.login(request).pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
            next: ((res : LoginResponseDto) => {
                // localStorage.setItem(ACCESS_TOKEN, res.access_token);
                // localStorage.setItem(REFRESH_TOKEN, res.refresh_token);
                this.tokenService.saveToken(res.access_token);
                this.tokenService.saveRefreshToken(res.refresh_token);
                this.toggleBlockUI(true)
                this.router.navigate(['']);
            }),
            error: ((ex) => {
                this.notificationService.showError("Đăng nhập không thành công")
                this.toggleBlockUI(false)
            })
        }) 
    }

    private toggleBlockUI(enabled : boolean)
  {
    if(enabled == true)
    {
      this.blockedPanel = true;
    }
    else
    {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }

    ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}