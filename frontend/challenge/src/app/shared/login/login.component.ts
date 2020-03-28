import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

import { UserLogin, Session } from '../../models/model';
import { FormControl, Validators } from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService) { }


  userName = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)] );

  ngOnInit(): void {
  }

  logIn() {
    const userLogin = new UserLogin();
    userLogin.username = this.userName.value;
    userLogin.password = this.password.value;

    this.authService.login(userLogin).subscribe((token: string) => localStorage.setItem('token', token));
  }

  SetTestUser() {
    this.userName.setValue('cursus.non@egetvariusultrices.org');
    this.password.setValue('Password');
  }

}
