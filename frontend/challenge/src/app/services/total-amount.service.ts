import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TotalAmountService {

  totalAmount: number;

  constructor() {
    this.totalAmount = 0;
  }

  AddTotalAmount(amount: number) {
    this.totalAmount += amount;
  }

  SubtractAmmount(amount: number) {
    this.totalAmount -= amount;
  }

  GetTotalAmount() {
    return this.totalAmount;
  }

}
