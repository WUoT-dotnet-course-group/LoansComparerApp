import { Component, OnDestroy, OnInit } from '@angular/core';
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
import { LoansComparerService } from '../../shared/services/loans-comparer/loans-comparer.service';
import { InquireDataStorageService } from '../services/inquire-data-storage.service';
// import { Subscription } from 'rxjs';

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
export class ReviewOffersComponent implements OnInit, OnDestroy {
  offers!: ReviewOffer[];
  selectedOffer: ReviewOffer | null = null;

  displayedColumns: string[] = [
    'bankName',
    'percentage',
    'monthlyInstallment',
    'loanPeriod',
    'loanValue',
  ];

  // offersSubscription!: Subscription;

  constructor(
    protected offerProviderService: OfferProviderService,
    protected inquireDataStorageService: InquireDataStorageService,
    protected loansComparerService: LoansComparerService
  ) {}

  ngOnInit(): void {
    this.offers = this.offerProviderService.offers;
    // this.offersSubscription = this.offerProviderService.offerCreated.subscribe(
    //   (offer: ReviewOffer) => {
    //     console.log(offer);
    //     this.offers.push(offer);
    //   }
    // );
  }

  onRowSelected(offer: ReviewOffer): void {
    this.selectedOffer = offer;
    this.inquireDataStorageService.selectedOffer = offer;

    this.loansComparerService.chooseOffer(
      this.offerProviderService.inquiryId!,
      {
        offerId: offer.id,
        bankId: offer.bankId,
      }
    );
  }

  ngOnDestroy(): void {
    // this.offersSubscription.unsubscribe();
  }
}
