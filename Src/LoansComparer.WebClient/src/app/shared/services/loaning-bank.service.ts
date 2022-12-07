import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface PersonalData {
  firstName: string;
  lastName: string;
  birthDate: Date;
}

export interface SelectType {
  id: number;
  name: string;
  description: string;
}

export interface InquiryDetails {
  loanValue: number;
  installmentsNumber: number;
  personalData: PersonalData;
  governmentDocument: SelectType;
  jobDetails: SelectType;
}

@Injectable({
  providedIn: 'root',
})
export class LoaningBankService {
  constructor(private http: HttpClient) {}

  createInquiry(inquiryDetails: InquiryDetails) {}
}
