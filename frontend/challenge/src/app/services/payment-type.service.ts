import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { PagedRequest } from '../models/PagedRequest';
import { PagedResult } from '../models/PagedResult';
import { PaymentType } from '../models/PaymentType';

@Injectable({
  providedIn: 'root'
})
export class PaymentTypeService {
  API_URL: string = environment.apiUrl + '/PaymentType';
  constructor(
    private http: HttpClient
  ) { }

  getPaymentTypes(): Observable<PaymentType[]> {
    // TODO implement cache.
    return this.http.get<PaymentType[]>(this.API_URL)
    .pipe(
      retry(3)
    );
  }

}
