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
import { InquiryHistoryDataSource } from './inquiry-history-data-source';
import { mergeAll } from 'rxjs/operators';
import { of } from 'rxjs';

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
export class InquiryHistoryComponent implements AfterViewInit, OnInit {
  defaultPageSize = 4;

  displayedColumns: string[] = [
    'indexer',
    'loanValue',
    'installments',
    'inquireDate',
    'chosenBank',
  ];

  dataSource!: InquiryHistoryDataSource;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private loansComparerService: LoansComparerService) {}

  ngOnInit(): void {
    this.dataSource = new InquiryHistoryDataSource(this.loansComparerService);
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
