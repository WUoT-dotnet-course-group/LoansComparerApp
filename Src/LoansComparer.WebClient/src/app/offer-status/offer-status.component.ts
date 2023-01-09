import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

export interface OfferDetails {
  percentage: number;
  monthlyInstallment: number;
  requestedValue: number;
  requestedPeriodInMonth: number;
  statusId: number;
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
export class OfferStatusComponent implements OnInit {
  offer: OfferDetails = {
    percentage: 12.58,
    monthlyInstallment: 5.08,
    requestedValue: 10,
    requestedPeriodInMonth: 2,
    statusId: 1,
    statusDescription: 'Created',
    createDate: new Date('2022-12-01T23:44:12.9922698'),
    updateDate: new Date('2022-12-01T23:44:12.9922698'),
    approvedBy: null,
    bankName: 'BNP',
  };

  constructor() {}

  ngOnInit(): void {}
}
