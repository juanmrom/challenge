import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './shared/login/login.component';
import { AuthGuardService } from './services/auth-guard.service';
import { HomeComponent } from './home/home.component';
import { AddPaymentComponent } from './add-payment/add-payment.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'addPayment', component: AddPaymentComponent },
  { path: '', component: HomeComponent, canActivate: [AuthGuardService] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
