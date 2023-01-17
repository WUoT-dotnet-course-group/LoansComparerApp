import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import {
  LoansComparerService,
  PagingParameter,
} from '../../shared/services/loans-comparer/loans-comparer.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { mergeAll } from 'rxjs/operators';
import { of } from 'rxjs';
import { BankOffersDataSource } from './bank-offers-data-source';

@Component({
  selector: 'app-bank-offers',
  templateUrl: './bank-offers.component.html',
  styleUrls: ['./bank-offers.component.less'],
  animations: [
    trigger('enterAndLeave', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':leave', [animate('0.4s ease-in')]),
      transition(':enter', [animate('0.4s 0.4s ease-in')]),
    ]),
  ],
})
export class BankOffersComponent implements AfterViewInit, OnInit {
  defaultPageSize = 10;

  displayedColumns: string[] = [
    'indexer',
    'loanValue',
    'numberOfInstallments',
    'status',
    'percentage',
    'offerCreateDate',
    'offerUpdateDate',
    'approvedBy',
  ];

  dataSource!: BankOffersDataSource;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private loansComparerService: LoansComparerService) {}

  ngOnInit(): void {
    this.dataSource = new BankOffersDataSource(this.loansComparerService);
    this.dataSource.loadInquiries(<PagingParameter>{
      sortOrder: '',
      sortHeader: '',
      pageIndex: 0,
      pageSize: this.defaultPageSize,
    });
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    of(this.sort.sortChange, this.paginator.page)
      .pipe(mergeAll())
      .subscribe(() => this.loadData());
  }

  loadData(): void {
    this.dataSource.loadInquiries(<PagingParameter>{
      sortOrder: this.sort.direction,
      sortHeader: this.sort.active,
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize,
    });
  }
}
