import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

import { UserLogin } from '../../models/UserLogin';
import { Session } from "../../models/Session";
import { FormControl, Validators } from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  userName = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)] );

  ngOnInit(): void {
  }

  logIn() {
    const userLogin = new UserLogin();
    userLogin.username = this.userName.value;
    userLogin.password = this.password.value;

    this.authService.login(userLogin).subscribe(
      (token: string) => localStorage.setItem('token', token),
      err => console.log(err),
      () => this.router.navigate(['/home'])
    );
  }

  SetTestUser() {
    this.userName.setValue('nisl@dui.org');
    this.password.setValue('Password');
  }

}
