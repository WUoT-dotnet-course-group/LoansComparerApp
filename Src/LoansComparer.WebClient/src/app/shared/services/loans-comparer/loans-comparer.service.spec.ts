import { TestBed } from '@angular/core/testing';

import { LoansComparerService } from './loans-comparer.service';

describe('LoansComparerService', () => {
  let service: LoansComparerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoansComparerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
