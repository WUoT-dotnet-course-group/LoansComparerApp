import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

export interface Offer {
  id: number;
  percentage: number;
  monthlyInstallment: number;
  requestedValue: number;
  requestedPeriodInMonth: number;
  statusId: number;
  statusDescription: string;
  inquireId: number;
  createDate: Date;
  updateDate: Date;
  approvedBy: string | undefined;
  documentLink: string;
  documentLinkValidDate: Date;
  bankName: string;
}

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
  offers: Offer[] = [
    {
      id: 38,
      percentage: 12.58,
      monthlyInstallment: 5.08,
      requestedValue: 10,
      requestedPeriodInMonth: 2,
      statusId: 1,
      statusDescription: 'Created',
      inquireId: 27,
      createDate: new Date('2022-12-01T23:44:12.9922698'),
      updateDate: new Date('2022-12-01T23:44:12.9922698'),
      approvedBy: undefined,
      documentLink:
        'https://mini.loanbank.api.snet.com.pl/api/v1/Offer/38/document/2bf467e7-2b7a-45f0-8852-ddfeb1ac0b3d',
      documentLinkValidDate: new Date('2023-06-01T23:44:12.9935121'),
      bankName: 'BNP',
    },
    {
      id: 37,
      percentage: 13.58,
      monthlyInstallment: 4.08,
      requestedValue: 100,
      requestedPeriodInMonth: 1,
      statusId: 1,
      statusDescription: 'Created',
      inquireId: 27,
      createDate: new Date('2022-12-02T23:44:12.9922698'),
      updateDate: new Date('2022-12-02T23:44:12.9922698'),
      approvedBy: undefined,
      documentLink:
        'https://mini.loanbank.api.snet.com.pl/api/v1/Offer/38/document/2bf467e7-2b7a-45f0-8852-ddfeb1ac0b3d',
      documentLinkValidDate: new Date('2023-06-02T23:44:12.9935121'),
      bankName: 'ING',
    },
  ];
  displayedColumns: string[] = [
    'bankName',
    'percentage',
    'monthlyInstallment',
    'loanPeriod',
    'loanValue',
  ];
  selectedOffer: Offer | null = null;

  constructor() {}

  ngOnInit(): void {}
}
