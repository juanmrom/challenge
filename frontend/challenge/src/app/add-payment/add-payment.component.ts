import { Component, OnInit, Inject } from '@angular/core';
import { PaymentType } from '../models/PaymentType';
import { PaymentTypeService } from '../services/payment-type.service';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Payment } from '../models/Payment';
import { TotalAmountService } from '../services/total-amount.service';
import { PaymentService } from '../services/payment.service';
import { TotalAmount } from '../models/TotalAmount';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
// import { Router } from '@angular/router';

@Component({
  selector: 'app-add-payment',
  templateUrl: './add-payment.component.html',
  styleUrls: ['./add-payment.component.scss']
})
export class AddPaymentComponent implements OnInit {

  paymentForm: FormGroup;
  paymentTypesList: PaymentType[];
  isEdit: boolean;

  constructor(
    private paymentTypeService: PaymentTypeService,
    private totalAmountService: TotalAmountService,
    private paymentService: PaymentService,
    // private router: Router
    public dialogRef: MatDialogRef<AddPaymentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Payment
  ) {
    this.isEdit = this.data != null;
  }

  ngOnInit(): void {
    this.createForm();
    this.loadPaymentType();
    if (this.isEdit) {
      this.paymentTypes.setValue(this.data.paymentTypeId);
      this.paymentDate.setValue(this.data.paymentDate);
      this.paymentPlace.setValue(this.data.paymentTypeName);
      this.paymentAmount.setValue(this.data.amount);
      this.paymentForm.updateValueAndValidity();
    }
  }

  createForm() {
    this.paymentForm = new FormGroup({
      paymentTypes: new FormControl('', [Validators.required]),
      paymentDate: new FormControl('', [Validators.required]),
      paymentPlace: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      paymentAmount: new FormControl('', [Validators.required, Validators.min(0), Validators.pattern(/^\d+(\.\d{1,2})?$/)])
    });
  }

  loadPaymentType() {
    this.paymentTypeService.getPaymentTypes().subscribe(
      (result: PaymentType[]) => {
      this.paymentTypesList = result;
    },
    err => console.log(err)
);
  }

  close() {
    this.dialogRef.close('Saved the payment');
  }

  edit() {
    if (this.paymentForm.pristine || this.paymentForm.invalid) {
      return;
    }
    const paymentUpdate = this.getPaymentData();
    paymentUpdate.id = this.data.id;
    this.paymentService.updatePayment(paymentUpdate).subscribe (
      (result: Payment) => {
        const totalAmount = new TotalAmount();
        totalAmount.amount = paymentUpdate.amount;
        totalAmount.paymentId = paymentUpdate.id;
        this.totalAmountService.UpdateTotalAmount(totalAmount);
      }
    );
    this.data = paymentUpdate;
    this.close();
  }

  add() {
    if (this.paymentForm.pristine || this.paymentForm.invalid) {
      return;
    }

    this.paymentService.addPayment(this.getPaymentData()).subscribe (
      (result: Payment) => {
        const totalAmount = new TotalAmount();
        totalAmount.amount = result.amount;
        totalAmount.paymentId = result.id;
        this.totalAmountService.UpdateTotalAmount(totalAmount);
      }
    );
    this.close();
  }

  getPaymentData(): Payment {
    const paymentData = new Payment();
    paymentData.amount = this.paymentAmount.value;
    paymentData.paymentTypeId = this.paymentTypes.value;
    paymentData.paymentTypeName = this.paymentPlace.value;
    paymentData.paymentDate = this.paymentDate.value;
    console.log(paymentData);
    return paymentData;
  }

  get paymentTypes() { return this.paymentForm.get('paymentTypes'); }
  get paymentDate() { return this.paymentForm.get('paymentDate'); }
  get paymentPlace() { return this.paymentForm.get('paymentPlace'); }
  get paymentAmount() { return this.paymentForm.get('paymentAmount'); }

}
