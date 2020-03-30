import { Component, OnInit, Input, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Observable } from 'rxjs';
import { TotalAmount } from 'src/app/models/TotalAmount';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavComponent implements OnInit {

  @Input() title: string;
  @Input() totalAmounts: Observable<number>;

  totalAmount = 0;

  constructor(
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.totalAmounts.subscribe(amount => {
      this.totalAmount = amount;
      this.cd.markForCheck();
    });
  }

}
