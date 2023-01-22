import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  BehaviorSubject,
  catchError,
  empty,
  forkJoin,
  map,
  Observable,
  of,
} from 'rxjs';
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
  private _offersToFetch: { [key: string]: string } = {};

  offersFetched$ = new BehaviorSubject<ReviewOffer[]>([]);

  constructor(
    private inquireDataStorageService: InquireDataStorageService,
    private loansComparerService: LoansComparerService,
    private router: Router
  ) {}

  get wasAllFetched(): boolean {
    return Object.keys(this._offersToFetch).length === 0;
  }

  initOffers(inquiryCreateResponse: CreateInquiryResponseDTO): void {
    this.inquireDataStorageService.inquiryId = inquiryCreateResponse.inquiryId;
    this._offersToFetch = inquiryCreateResponse.bankInquiries;
    this.router.navigateByUrl('/inquire/offers');
  }

  fetchOffers(): void {
    const requests: Observable<OfferDTO | null>[] = [];

    for (let bankId in this._offersToFetch) {
      requests.push(
        this.loansComparerService
          .fetchOffer(bankId, this._offersToFetch[bankId])
          .pipe(catchError((_) => of(null)))
      );
    }

    forkJoin(requests)
      .pipe(
        map((offer) => <ReviewOffer[]>offer.filter((value) => value !== null))
      )
      .subscribe((offers: ReviewOffer[]) => {
        offers.forEach((offer) => delete this._offersToFetch[offer.bankId]);
        this.offersFetched$.next(offers);
      });
  }

  cleanOffers(): void {
    this._offers = [];
  }
}
