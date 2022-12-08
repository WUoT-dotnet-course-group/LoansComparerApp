import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

export interface InquiryHistoryRow {
  loanValue: number;
  inquireDate: Date;
  bankOfChosenOffer: string;
  offerStatus: string;
}

@Component({
  selector: 'app-inquiry-history',
  templateUrl: './inquiry-history.component.html',
  styleUrls: ['./inquiry-history.component.less'],
  animations: [
    trigger('enterAndLeave', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':leave', [animate('0.4s ease-in')]),
      transition(':enter', [animate('0.4s 0.4s ease-in')]),
    ]),
  ],
})
export class InquiryHistoryComponent implements OnInit {
  inquiries: InquiryHistoryRow[] = [
    {
      loanValue: 12000,
      inquireDate: new Date(),
      bankOfChosenOffer: 'BNP',
      offerStatus: 'Pending',
    },
    {
      loanValue: 3000,
      inquireDate: new Date(),
      bankOfChosenOffer: 'ING',
      offerStatus: 'Accepted',
    },
    {
      loanValue: 6500,
      inquireDate: new Date(),
      bankOfChosenOffer: 'Pekao',
      offerStatus: 'Rejected',
    },
    {
      loanValue: 12000,
      inquireDate: new Date(),
      bankOfChosenOffer: 'BNP',
      offerStatus: 'Pending',
    },
    {
      loanValue: 3000,
      inquireDate: new Date(),
      bankOfChosenOffer: 'ING',
      offerStatus: 'Accepted',
    },
    {
      loanValue: 3000,
      inquireDate: new Date(),
      bankOfChosenOffer: 'ING',
      offerStatus: 'Accepted',
    },
  ];
  displayedColumns: string[] = [
    'loanValue',
    'inquireDate',
    'offerStatus',
    'bankOfChosenOffer',
  ];

  constructor() {}

  ngOnInit(): void {}
}
