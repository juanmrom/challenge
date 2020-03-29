import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Payment } from '../models/Payment';
import { PagedResult } from '../models/PagedResult';
import { PagedRequest } from '../models/PagedRequest';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  API_URL: string = environment.apiUrl + '/Payment/GetPayments';

  constructor(
    private http: HttpClient
  ) { }

  getPayments(pagedRequest: PagedRequest): Observable<PagedResult<Payment>> {
    return this.http.post<PagedResult<Payment>>(this.API_URL,  pagedRequest)
    .pipe(
      retry(3)
    );
  }

}
