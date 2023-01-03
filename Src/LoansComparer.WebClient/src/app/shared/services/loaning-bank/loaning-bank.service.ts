import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

export interface JobDetails {
  typeId: number;
  name: string;
  description: string;
  jobStartDate: Date;
  jobEndDate: Date;
}

export interface InquiryDetails {
  loanValue: number;
  installmentsNumber: number;
  personalData: PersonalData;
  governmentDocument: GovernmentDocument;
  jobDetails: JobDetails;
}

export interface PersonalDataDTO {
  firstName: string;
  lastName: string;
  governmentId: string;
  governmentIdType: number;
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

@Injectable({
  providedIn: 'root',
})
export class LoaningBankService {
  private path: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createInquiry(inquiryDetails: InquiryDetails): void {
    this.http
      .post<any>(this.path + 'api/inquiries/add', {
        loanValue: inquiryDetails.loanValue,
        numberOfInstallments: inquiryDetails.installmentsNumber,
        personalData: {
          firstName: inquiryDetails.personalData.firstName,
          lastName: inquiryDetails.personalData.lastName,
          governmentId: inquiryDetails.governmentDocument.number,
          governmentIdType: inquiryDetails.governmentDocument.typeId - 1,
        },
      })
      .subscribe((_) => {});
  }

  saveUserData(userData: SaveUserData): void {
    this.http
      .post<any>(this.path + 'api/users/save-data', userData)
      .subscribe((_) => {});
  }

  getInquiries(): Observable<GetInquiryData[]> {
    return this.http.get<GetInquiryData[]>(this.path + 'api/inquiries');
  }
}
