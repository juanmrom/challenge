import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';



import { Payment } from '../models/Payment';
import { PagedResult } from '../models/PagedResult';
import { PagedRequest } from '../models/PagedRequest';
import { PaymentService } from '../services/payment.service';
import { Router } from '@angular/router';
import { TotalAmountService } from '../services/total-amount.service';
import { TotalAmount } from '../models/TotalAmount';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddPaymentComponent } from '../add-payment/add-payment.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  displayedColumns: string[] = [
    'paymentTypeName'
    , 'placeName'
    , 'amount'
    , 'update'

  ];
  pagedResquest = new PagedRequest();
  payments = new MatTableDataSource<Payment>();
  currentPage = 1;
  rowCount = 100;
  pageSize = 10;
  dialogConfig = new MatDialogConfig();

  constructor(
    private paymentService: PaymentService,
    private router: Router,
    private totalAmountService: TotalAmountService,
    private matDialog: MatDialog
  ) {
    this.dialogConfig.height = '450px';
    this.dialogConfig.width = '400px';
  }

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  ngOnInit(): void {
    this.getPayments();
  }

  public getPaginatorData(e: any) {
    console.log(e);
    this.currentPage = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getPayments();
  }

  getPayments() {
    this.pagedResquest.currentPage = this.currentPage;
    this.pagedResquest.pageSize = this.pageSize;
    this.paymentService.getPayments(this.pagedResquest).subscribe(
      (result: PagedResult<Payment>) => {
        this.payments.data = result.results;
        this.rowCount = result.rowCount;
        this.pageSize = result.pageSize;
        this.currentPage = result.currentPage;
        this.payments._updateChangeSubscription();
      },
      err => console.log(err)
    );
  }

  deletePayment(row?: Payment) {
    this.paymentService.deletePayment(row.id).subscribe(
      (res: any) => {
        const deleteTotal = new TotalAmount();
        deleteTotal.amount = row.amount;
        deleteTotal.paymentId = row.id;
        this.getPayments();
        this.totalAmountService.DeleteTotalAmount(deleteTotal);
      },
      err => console.log(err)
    );
  }

  updatePayment(row?: Payment) {
    console.log(row);
    this.dialogConfig.data = row;
    this.matDialog.open(AddPaymentComponent, this.dialogConfig).afterClosed().subscribe( value => {
      this.getPayments();
      this.dialogConfig.data = null;
    });
  }

  addPaymentDialog() {
    this.matDialog.open(AddPaymentComponent, this.dialogConfig).afterClosed().subscribe( value => {
      this.dialogConfig.data = null;
    });
  }

}
