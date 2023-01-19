import {
  LoansComparerService,
  PagingParameter,
  PaginatedResponse,
  OfferDTO,
} from '../../shared/services/loans-comparer/loans-comparer.service';
import { BaseDataSource } from '../../shared/services/loans-comparer/base-data-source';

export interface GetInquiryData {
  loanValue: number;
  numberOfInstallments: number;
  inquireDate: Date;
  chosenBank: string;
}

export class BankOffersDataSource extends BaseDataSource<OfferDTO> {
  totalNumberOfInquiries = 0;

  constructor(protected override loansComparerService: LoansComparerService) {
    super(loansComparerService);
  }

  loadInquiries(pagingParams: PagingParameter): void {
    this.loansComparerService
      .getBankOffers(pagingParams)
      .subscribe((response: PaginatedResponse<OfferDTO>) => {
        this.totalNumberOfInquiries = response.totalNumber;
        this.data.next(response.items);
      });
  }
}
