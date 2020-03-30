import { Injectable } from '@angular/core';
import { PaymentService } from './payment.service';
import { TotalAmount } from '../models/TotalAmount';
import { Subject, Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class TotalAmountService {

  totalAmount = new Subject<number>();
  totalAmounts: TotalAmount[];

  constructor(
    private paymentsService: PaymentService
  ) {
    this.loadTotal();
  }

  private loadTotal() {
    this.paymentsService.getTotalAmount().subscribe(
      (result: TotalAmount[]) => {
        this.totalAmounts = result;
        this.calculateTotalAmount();
      }
    );
  }

  private calculateTotalAmount() {
    let result = 0;
    this.totalAmounts.forEach(element => {
      result += element.amount;
    });
    this.totalAmount.next(result);
  }

  UpdateTotalAmount(amount: TotalAmount) {
    const updateAmount = this.totalAmounts.find(t => t.paymentId === amount.paymentId);
    if (updateAmount != null) {
      updateAmount.amount = amount.amount;
    } else {
      this.totalAmounts.push(amount);
    }
    this.calculateTotalAmount();
  }

  DeleteTotalAmount(amount: TotalAmount) {
    const indexAmount = this.totalAmounts.findIndex(t => t.paymentId === amount.paymentId);
    this.totalAmounts.splice(indexAmount, 1);
    this.calculateTotalAmount();
  }

  // SubtractAmmount(amount: number) {
  //   this.totalAmount -= amount;
  // }

  GetTotalAmount(): Observable<number> {
    return this.totalAmount.asObservable();
  }

}
