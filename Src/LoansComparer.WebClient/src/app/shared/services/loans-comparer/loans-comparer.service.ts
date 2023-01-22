import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetInquiryData } from '../../../home/inquiry-history/inquiry-history-data-source';

export interface GovernmentDocumentDTO {
  governmentIdType: DictionaryDTO;
  governmentId: string;
}

export interface JobDetailsDTO {
  jobType: DictionaryDTO;
  jobStartDate: Date;
  jobEndDate: Date;
}

export interface CreateInquiryDTO {
  loanValue: number;
  numberOfInstallments: number;
  personalData: PersonalDataDTO;
}

export interface PersonalDataDTO {
  firstName: string;
  lastName: string;
  birthDate: Date;
  email: string | null;
  governmentDocument: GovernmentDocumentDTO;
  jobDetails: JobDetailsDTO;
}

export interface CreateInquiryResponseDTO {
  inquiryId: string;
  bankInquiries: { [key: string]: string };
}

export interface DictionaryDTO {
  typeId: number;
  name: string | null;
}

export interface OfferDTO {
  id: string;
  percentage: number;
  monthlyInstallment: number;
  loanValue: number;
  loanPeriod: number;
  status: number;
  statusDescription: string;
  inquiryId: string;
  createDate: Date;
  updateDate: Date;
  approvedBy: string;
  documentLink: string | null;
  documentLinkValidDate: Date | null;
  bankId: string;
  bankName: string;
}

export interface ChooseOfferDTO {
  offerId: string;
  bankId: string;
}

export interface PagingParameter {
  sortOrder: SortDirection;
  sortHeader: string;
  pageIndex: number;
  pageSize: number;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalNumber: number;
}

@Injectable({
  providedIn: 'root',
})
export class LoansComparerService {
  private path: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createInquiry(
    createInquiryData: CreateInquiryDTO
  ): Observable<CreateInquiryResponseDTO> {
    return this.http.post<CreateInquiryResponseDTO>(
      this.path + 'api/inquiries/create',
      createInquiryData
    );
  }

  getUserData(): Observable<PersonalDataDTO> {
    return this.http.get<PersonalDataDTO>(this.path + 'api/users/data/get');
  }

  saveUserData(userData: PersonalDataDTO): void {
    this.http
      .post<any>(this.path + 'api/users/data/save', userData)
      .subscribe();
  }

  getInquiries(
    request: PagingParameter
  ): Observable<PaginatedResponse<GetInquiryData>> {
    return this.http.get<PaginatedResponse<GetInquiryData>>(
      this.path + 'api/inquiries',
      {
        params: new HttpParams()
          .set('sortOrder', request.sortOrder)
          .set('sortHeader', request.sortHeader)
          .set('pageIndex', request.pageIndex)
          .set('pageSize', request.pageSize),
      }
    );
  }

  fetchOffer(bankId: string, bankInquiryId: string): Observable<OfferDTO> {
    return this.http.get<OfferDTO>(
      this.path + `api/inquiries/fetch-offer/${bankId}/${bankInquiryId}`
    );
  }

  getInquiryOffer(bankId: string, offerId: string): Observable<OfferDTO> {
    return this.http.get<OfferDTO>(
      this.path + `api/offers/${bankId}/${offerId}`
    );
  }

  chooseOffer(inquiryId: string, chooseOfferData: ChooseOfferDTO): void {
    this.http
      .patch<any>(
        this.path + `api/inquiries/${inquiryId}/choose-offer`,
        chooseOfferData
      )
      .subscribe();
  }

  uploadDocument(inquiryId: string, document: FormData): void {
    this.http
      .post<any>(this.path + `api/inquiries/${inquiryId}/upload`, document)
      .subscribe();
  }

  getBankOffers(
    request: PagingParameter
  ): Observable<PaginatedResponse<OfferDTO>> {
    return this.http.get<PaginatedResponse<OfferDTO>>(
      this.path + 'api/offers',
      {
        params: new HttpParams()
          .set('sortOrder', request.sortOrder)
          .set('sortHeader', request.sortHeader)
          .set('pageIndex', request.pageIndex)
          .set('pageSize', request.pageSize),
      }
    );
  }

  acceptOffer(offerId: string): Observable<any> {
    return this.http.patch<any>(this.path + `api/offers/${offerId}/accept`, {});
  }

  rejectOffer(offerId: string): Observable<any> {
    return this.http.patch<any>(this.path + `api/offers/${offerId}/reject`, {});
  }

  getJobTypes(): Observable<DictionaryDTO[]> {
    return this.http.get<DictionaryDTO[]>(
      this.path + 'api/dictionary/job-types'
    );
  }

  getGovernmentIdTypes(): Observable<DictionaryDTO[]> {
    return this.http.get<DictionaryDTO[]>(
      this.path + 'api/dictionary/government-id-types'
    );
  }

  getInquiriesAmount(): Observable<number> {
    return this.http.get<number>(this.path + 'api/inquiries/total');
  }
}
