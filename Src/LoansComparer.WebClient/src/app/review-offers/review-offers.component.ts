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
} from '../shared/services/providers/offer-provider.service';
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

  constructor(protected offerProviderService: OfferProviderService) {}

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
  }

  ngOnDestroy(): void {
    // this.offersSubscription.unsubscribe();
  }
}
