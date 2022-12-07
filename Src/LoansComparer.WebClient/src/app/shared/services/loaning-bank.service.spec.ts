import { TestBed } from '@angular/core/testing';

import { LoaningBankService } from './loaning-bank.service';

describe('LoaningBankService', () => {
  let service: LoaningBankService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoaningBankService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
