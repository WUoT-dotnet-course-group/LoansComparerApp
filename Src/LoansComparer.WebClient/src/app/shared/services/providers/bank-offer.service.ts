import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import {
  CreateInquiryResponseDTO,
  LoansComparerService,
  OfferDTO,
} from '../loans-comparer/loans-comparer.service';

@Injectable({
  providedIn: 'root',
})
export class OfferProviderService {
  inquiryId: string | null = null;
  inquiryCreated = new Subject<CreateInquiryResponseDTO>();

  offers: OfferDTO[] = [];

  constructor(
    private loansComparerService: LoansComparerService,
    private router: Router
  ) {
    this.inquiryCreated.subscribe((event: CreateInquiryResponseDTO) => {
      this.inquiryId = event.inquiryId;
      this.loansComparerService
        .getOffer(event.bankInquiryId)
        .subscribe((offer) => {
          this.offers.push(offer);
          this.router.navigateByUrl('/offers');
        });
    });
  }
}
