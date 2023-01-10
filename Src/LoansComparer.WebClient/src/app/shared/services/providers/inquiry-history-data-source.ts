import { finalize } from 'rxjs';
import {
  LoansComparerService,
  PagingParameter,
  PaginatedResponse,
} from '../loans-comparer/loans-comparer.service';
import { BaseDataSource } from './base-data-source';

export interface GetInquiryData {
  loanValue: number;
  inquireDate: Date;
  chosenBank: string;
  offerStatus: string;
}

export class InquiryHistoryDataSource extends BaseDataSource<GetInquiryData> {
  totalNumberOfInquiries = 0;

  constructor(protected override loansComparerService: LoansComparerService) {
    super(loansComparerService);
  }

  loadInquiries(pagingParams: PagingParameter): void {
    this.loading.next(true);

    this.loansComparerService
      .getInquiries(pagingParams)
      .pipe(finalize(() => this.loading.next(false)))
      .subscribe((response: PaginatedResponse<GetInquiryData>) => {
        this.totalNumberOfInquiries = response.totalNumber;
        this.data.next(response.items);
      });
  }
}
