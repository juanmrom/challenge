import { Component, OnInit } from '@angular/core';
import { PaymentType } from '../models/PaymentType';
import { PaymentTypeService } from '../services/payment-type.service';

@Component({
  selector: 'app-add-payment',
  templateUrl: './add-payment.component.html',
  styleUrls: ['./add-payment.component.scss']
})
export class AddPaymentComponent implements OnInit {

  paymentTypes: PaymentType[];
  constructor(
    private paymentTypeService: PaymentTypeService
  ) { }

  ngOnInit(): void {
    this.paymentTypeService.getPaymentTypes().subscribe(
          (result: PaymentType[]) => {
          this.paymentTypes = result;
        },
        err => console.log(err)
    );
  }

}
