import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { SelectionModel } from '@angular/cdk/collections';

import { Payment } from '../models/Payment';
import { PagedResult } from '../models/PagedResult';
import { PagedRequest } from '../models/PagedRequest';
import { PaymentService } from '../services/payment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  displayedColumns: string[] = [
    'select'
    , 'paymentTypeName'
    , 'placeName'
    , 'amount'
  ];
  pagedResquest = new PagedRequest();
  payments = new MatTableDataSource<Payment>();
  currentPage = 1;
  rowCount = 100;
  pageSize = 10;
  selection = new SelectionModel<Payment>(true, []);

  constructor(
    private paymentService: PaymentService,
    private router: Router
  ) { }

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  ngOnInit(): void {
    this.getPayments();
  }

  addPayment() {
    this.router.navigate(['/addPayment']);
  }

  editPayment() {
    console.log('edit');
  }

  removePayment() {
    console.log('remove');
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
      },
      err => console.log(err)
    );
  }

  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.payments.data.forEach(row => this.selection.select(row));
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.payments.data.length;
    return numSelected === numRows;
  }

  checkboxLabel(row?: Payment): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

}
