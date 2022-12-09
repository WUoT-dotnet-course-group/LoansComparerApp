import { Component, OnInit, ViewChild } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import {
  GetInquiryData,
  LoansComparerService,
} from '../shared/services/loans-comparer/loans-comparer.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

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
  dataSource = new MatTableDataSource<GetInquiryData>(this.inquiries);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private loansComparerService: LoansComparerService) {}

  ngOnInit(): void {
    this.loansComparerService
      .getInquiries()
      .subscribe((response: GetInquiryData[]) => (this.inquiries = response));
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}
