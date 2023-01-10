import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import {
  CreateInquiryResponseDTO,
  LoansComparerService,
  OfferDTO,
} from '../loans-comparer/loans-comparer.service';

export interface ReviewOffer {
  id: string;
  percentage: number;
  monthlyInstallment: number;
  loanValue: number;
  loanPeriod: number;
  status: number;
  statusDescription: string;
  inquiryId: string;
  createDate: Date;
  updateDate: Date;
  approvedBy: string | null;
  documentLink: string;
  documentLinkValidDate: Date;
  bankName: string;
  bankId: string;
}

@Injectable({
  providedIn: 'root',
})
export class OfferProviderService {
  private _inquiryId: string | null = null;

  offers: ReviewOffer[] = [];
  inquiryCreated = new Subject<CreateInquiryResponseDTO>();
  // offerCreated = new Subject<ReviewOffer>();

  constructor(
    private loansComparerService: LoansComparerService,
    private router: Router
  ) {
    this.inquiryCreated.subscribe((event: CreateInquiryResponseDTO) => {
      this._inquiryId = event.inquiryId;
      this.loansComparerService
        .getOffer(event.bankInquiryId)
        .subscribe((offer: OfferDTO) => {
          // this.offerCreated.next(offer);
          this.offers.push(offer);
          this.router.navigateByUrl('/offers');
        });
    });
  }

  get inquiryId(): string | null {
    return this._inquiryId;
  }
}
