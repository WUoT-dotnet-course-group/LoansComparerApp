import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

@Injectable({
  providedIn: 'root',
})
export class LoaningBankService {
  private path: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createInquiry(inquiryDetails: InquiryDetails) {
    this.http
      .post(this.path + 'api/inquiries/add', {
        loanValue: inquiryDetails.loanValue,
        numberOfInstallments: inquiryDetails.installmentsNumber,
        personalData: {
          firstName: inquiryDetails.personalData.firstName,
          lastName: inquiryDetails.personalData.lastName,
          governmentId: inquiryDetails.governmentDocument.number,
          governmentIdType: inquiryDetails.governmentDocument.typeId - 1,
        },
      })
      .subscribe((response) => {
        console.log(response);
      });
  }
}
