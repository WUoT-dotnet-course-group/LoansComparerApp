import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  CreateInquiryResponseDTO,
  LoansComparerService,
  OfferDTO,
} from '../../shared/services/loans-comparer/loans-comparer.service';

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

@Injectable()
export class OfferProviderService {
  private _inquiryId: string | null = null;
  private _offers: ReviewOffer[] = [];

  // offerCreated = new Subject<ReviewOffer>();

  constructor(
    private loansComparerService: LoansComparerService,
    private router: Router
  ) {}

  get inquiryId(): string | null {
    return this._inquiryId;
  }

  get offers(): ReviewOffer[] {
    return this._offers;
  }

  fetchOffers(forInquiry: CreateInquiryResponseDTO): void {
    this._inquiryId = forInquiry.inquiryId;
    this.loansComparerService
      .getOffer(forInquiry.bankInquiryId)
      .subscribe((offer: OfferDTO) => {
        // this.offerCreated.next(offer);
        this._offers.push(offer);
        this.router.navigateByUrl('/inquire/offers');
      });
  }

  cleanOffers(): void {
    this._offers = [];
  }
}
