import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import {
  GetInquiryData,
  LoaningBankService,
} from '../shared/services/loaning-bank/loaning-bank.service';

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
  inquiries: GetInquiryData[] = [];
  displayedColumns: string[] = [
    'indexer',
    'loanValue',
    'inquireDate',
    'offerStatus',
    'bankOfChosenOffer',
  ];

  constructor(private loaningBankService: LoaningBankService) {}

  ngOnInit(): void {
    this.loaningBankService
      .getInquiries()
      .subscribe((response: GetInquiryData[]) => (this.inquiries = response));
  }
}
