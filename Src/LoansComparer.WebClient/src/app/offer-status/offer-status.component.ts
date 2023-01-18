import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import {
  LoansComparerService,
  OfferDTO,
} from '../shared/services/loans-comparer/loans-comparer.service';
import { AuthService } from '../shared/services/auth/auth.service';
import { LoadingService } from '../shared/services/loading/loading.service';

export interface OfferDetails {
  id: string;
  percentage: number;
  monthlyInstallment: number;
  loanValue: number;
  loanPeriod: number;
  status: number;
  statusDescription: string;
  createDate: Date;
  updateDate: Date;
  approvedBy: string | null;
  bankName: string;
}

@Component({
  selector: 'app-offer-status',
  templateUrl: './offer-status.component.html',
  styleUrls: ['./offer-status.component.css'],
  animations: [
    trigger('animation', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':enter', [animate('0.75s ease-in')]),
      transition(':leave', [animate('0.75s ease-in')]),
    ]),
  ],
})
export class OfferStatusComponent implements OnInit, OnDestroy {
  private routeSub!: Subscription;

  offer!: OfferDetails;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private loansComparerService: LoansComparerService,
    public loadingService: LoadingService
  ) {}

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe((params) => {
      this.loansComparerService
        .getInquiryOffer(params['offerId'])
        .subscribe(
          (response: OfferDTO) =>
            (this.offer = <OfferDetails>(<unknown>response))
        );
    });
  }

  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }

  get isBankEmployee(): boolean {
    return this.authService.isBankEmployee;
  }

  reject(): void {
    this.loansComparerService.rejectOffer(this.offer.id);
  }

  accept(): void {
    this.loansComparerService.acceptOffer(this.offer.id);
  }
}
