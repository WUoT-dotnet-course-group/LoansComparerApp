import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export enum GovernmentIdType {
  PESEL = 1,
  Passport = 2,
  ID = 3,
}

export interface PersonalData {
  firstName: string;
  lastName: string;
  birthDate: Date;
}

export interface GovernmentDocument {
  typeId: number;
  name: string;
  description: string;
  number: string;
}

export interface JobDetailsDTO extends DictionaryDTO {
  jobStartDate: Date;
  jobEndDate: Date;
}

export interface CreateInquiryDTO {
  loanValue: number;
  numberOfInstallments: number;
  personalData: PersonalDataDTO;
}

export interface CreateInquiryResponseDTO {
  inquiryId: string;
  bankInquiryId: string;
}

export interface PersonalDataDTO {
  firstName: string;
  lastName: string;
  birthDate: Date;
  governmentId: string;
  governmentIdType: number;
  jobDetails: JobDetailsDTO;
}

export interface SaveUserData {
  email: string;
  personalData: PersonalDataDTO;
}

export interface GetInquiryData {
  loanValue: number;
  inquireDate: Date;
  chosenBank: string;
  offerStatus: string;
}

export interface DictionaryDTO {
  typeId: number;
  name: string;
  description: string;
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
  documentLink: string;
  documentLinkValidDate: Date;
  bankName: string;
}

export interface ChooseOfferDTO {
  offerId: string;
  offerBankId: string;
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

  saveUserData(userData: PersonalDataDTO): void {
    this.http
      .post<any>(this.path + 'api/users/data/save', userData)
      .subscribe((_) => {});
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

  getOffer(bankInquiryId: string): Observable<OfferDTO> {
    return this.http.get<OfferDTO>(
      this.path + `api/inquiries/${bankInquiryId}/offer`
    );
  }

  chooseOffer(inquiryId: string, chooseOfferData: ChooseOfferDTO): void {
    this.http
      .patch<any>(
        this.path + `api/inquiries/${inquiryId}/choose-offer`,
        chooseOfferData
      )
      .subscribe((_) => {});
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
}
