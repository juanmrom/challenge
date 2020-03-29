import { TestBed } from '@angular/core/testing';

import { PaymentTypeService } from './payment-type.service';

describe('PaymentTypeServiceService', () => {
  let service: PaymentTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaymentTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
