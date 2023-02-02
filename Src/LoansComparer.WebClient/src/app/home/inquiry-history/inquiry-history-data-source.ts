import {
  LoansComparerService,
  PagingParameter,
  PaginatedResponse,
} from '../../shared/services/loans-comparer/loans-comparer.service';
import { BaseDataSource } from '../../shared/services/loans-comparer/base-data-source';

export interface GetInquiryData {
  loanValue: number;
  numberOfInstallments: number;
  inquireDate: Date;
  chosenBankId: string | null;
  chosenOfferId: string | null;
}

export class InquiryHistoryDataSource extends BaseDataSource<GetInquiryData> {
  totalNumberOfInquiries = 0;

  constructor(protected override loansComparerService: LoansComparerService) {
    super(loansComparerService);
  }

  loadInquiries(pagingParams: PagingParameter): void {
    this.loansComparerService
      .getInquiries(pagingParams)
      .subscribe((response: PaginatedResponse<GetInquiryData>) => {
        this.totalNumberOfInquiries = response.totalNumber;
        this.data.next(response.items);
      });
  }
}
