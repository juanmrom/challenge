import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Payment } from '../models/Payment';
import { PagedResult } from '../models/PagedResult';
import { PagedRequest } from '../models/PagedRequest';
import { TotalAmount } from '../models/TotalAmount';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  API_GET_PAYMENTS: string = environment.apiUrl + '/Payment/GetPayments';
  API_GET_TOTAL: string = environment.apiUrl + '/Payment/GetTotalAmount';
  API_ADD_PAYMENT: string = environment.apiUrl + '/Payment/AddPayment';
  API_DELETE_PAYMENT: string = environment.apiUrl + '/Payment/';
  API_UPDATE_PAYMENT: string = environment.apiUrl + '/Payment/';

  constructor(
    private http: HttpClient
  ) { }

  getPayments(pagedRequest: PagedRequest): Observable<PagedResult<Payment>> {
    return this.http.post<PagedResult<Payment>>(this.API_GET_PAYMENTS,  pagedRequest)
    .pipe(
      retry(3)
    );
  }

  getTotalAmount(): Observable<TotalAmount[]> {
    return this.http.get<TotalAmount[]>(this.API_GET_TOTAL)
      .pipe(
        retry(3)
      );
  }

  addPayment(payment: Payment) {
    return this.http.post<Payment>(this.API_ADD_PAYMENT, payment)
      .pipe(
        retry(3)
      );
  }

  deletePayment(paymentId: number) {
    return this.http.delete(this.API_DELETE_PAYMENT + paymentId)
      .pipe(
        retry(3)
      );
  }

  updatePayment(payment: Payment) {
    return this.http.put(this.API_UPDATE_PAYMENT + payment.id, payment)
      .pipe(
        retry(3)
      );
  }

}
