import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  CreateInquiryResponseDTO,
  LoansComparerService,
  OfferDTO,
} from '../../shared/services/loans-comparer/loans-comparer.service';
import { InquireDataStorageService } from './inquire-data-storage.service';

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
  documentLink: string | null;
  documentLinkValidDate: Date | null;
  bankName: string;
  bankId: string;
}

@Injectable()
export class OfferProviderService {
  private _offers: ReviewOffer[] = [];

  // offerCreated = new Subject<ReviewOffer>();

  constructor(
    private inquireDataStorageService: InquireDataStorageService,
    private loansComparerService: LoansComparerService,
    private router: Router
  ) {}

  get offers(): ReviewOffer[] {
    return this._offers;
  }

  fetchOffers(inquiryCreateResponse: CreateInquiryResponseDTO): void {
    this.inquireDataStorageService.inquiryId = inquiryCreateResponse.inquiryId;
    this.loansComparerService
      .fetchOffer(inquiryCreateResponse.bankInquiryId)
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
