import { Component, OnInit, Input } from '@angular/core';
import { TotalAmountService } from 'src/app/services/total-amount.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  @Input() title: string;
  totalAmount: number;

  constructor(private totalAmountService: TotalAmountService) {
    this.totalAmount = this.totalAmountService.GetTotalAmount();
  }

  ngOnInit(): void {
  }

}
