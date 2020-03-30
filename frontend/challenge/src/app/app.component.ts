import { Component, OnInit } from '@angular/core';
import { TotalAmount } from './models/TotalAmount';

import { BehaviorSubject, Observable } from 'rxjs';
import { newArray } from '@angular/compiler/src/util';
import { PaymentService } from './services/payment.service';
import { TotalAmountService } from './services/total-amount.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'challenge';

  totalAmounts$ = new BehaviorSubject<number>(0);

  constructor(
    private totalAmountService: TotalAmountService
  ) { }

  ngOnInit(): void {
    this.totalAmountService.GetTotalAmount().subscribe(
      (result: number) => {
        this.totalAmounts$.next(result);
      },
      err => console.log(err)
    );
  }

}
