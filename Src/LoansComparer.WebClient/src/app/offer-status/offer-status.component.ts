import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { Offer } from '../review-offers/review-offers.component';

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
export class OfferStatusComponent implements OnInit {
  offer: Offer = {
    id: '38',
    percentage: 12.58,
    monthlyInstallment: 5.08,
    requestedValue: 10,
    requestedPeriodInMonth: 2,
    statusId: 1,
    statusDescription: 'Created',
    inquireId: 27,
    createDate: new Date('2022-12-01T23:44:12.9922698'),
    updateDate: new Date('2022-12-01T23:44:12.9922698'),
    approvedBy: null,
    documentLink:
      'https://mini.loanbank.api.snet.com.pl/api/v1/Offer/38/document/2bf467e7-2b7a-45f0-8852-ddfeb1ac0b3d',
    documentLinkValidDate: new Date('2023-06-01T23:44:12.9935121'),
    bankName: 'BNP',
  };

  constructor() {}

  ngOnInit(): void {}
}
