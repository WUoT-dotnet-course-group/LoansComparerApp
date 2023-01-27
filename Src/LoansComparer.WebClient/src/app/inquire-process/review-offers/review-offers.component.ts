import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import {
  OfferProviderService,
  ReviewOffer,
} from '../services/offer-provider.service';
import {
  LoansComparerService,
  OfferDTO,
} from '../../shared/services/loans-comparer/loans-comparer.service';
import { InquireDataStorageService } from '../services/inquire-data-storage.service';
import { MatTableDataSource } from '@angular/material/table';
import { catchError, forkJoin, map } from 'rxjs';

@Component({
  selector: 'app-review-offers',
  templateUrl: './review-offers.component.html',
  styleUrls: ['./review-offers.component.less'],
  animations: [
    trigger('displayForm', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':enter', [animate('0.75s ease-in')]),
      transition(':leave', [animate('0.75s ease-in')]),
    ]),
  ],
})
export class ReviewOffersComponent implements OnInit {
  offers = new MatTableDataSource<ReviewOffer>();
  selectedOffer: ReviewOffer | null = null;

  displayedColumns: string[] = [
    'bankName',
    'percentage',
    'monthlyInstallment',
    'loanPeriod',
    'loanValue',
  ];

  constructor(
    protected inquireDataStorageService: InquireDataStorageService,
    protected loansComparerService: LoansComparerService,
    public offerProviderService: OfferProviderService
  ) {}

  ngOnInit(): void {
    this.offerProviderService.offersFetched$
      .asObservable()
      .subscribe((offers) => {
        this.offers = new MatTableDataSource<ReviewOffer>([
          ...this.offers.data,
          ...offers,
        ]);
      });

    this.onRefresh();
  }

  onRowSelected(offer: ReviewOffer): void {
    this.selectedOffer = offer;
    this.inquireDataStorageService.selectedOffer = offer;

    this.loansComparerService.chooseOffer(
      this.inquireDataStorageService.inquiryId!,
      {
        offerId: offer.id,
        bankId: offer.bankId,
      }
    );
  }

  onRefresh(): void {
    this.offerProviderService.fetchOffers();
  }
}
