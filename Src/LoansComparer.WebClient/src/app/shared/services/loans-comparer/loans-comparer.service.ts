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

export interface AddInquiryDTO {
  loanValue: number;
  numberOfInstallments: number;
  personalData: PersonalDataDTO;
}

export interface PersonalDataDTO {
  firstName: string;
  lastName: string;
  birthDate: Date;
  governmentId: string;
  governmentIdType: number;
  jobType: number;
  jobStartDate: Date;
  jobEndDate: Date;
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

@Injectable({
  providedIn: 'root',
})
export class LoansComparerService {
  private path: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createInquiry(addInquiryData: AddInquiryDTO): void {
    this.http
      .post<any>(this.path + 'api/inquiries/add', addInquiryData)
      .subscribe((_) => {});
  }

  saveUserData(userData: PersonalDataDTO): void {
    this.http
      .post<any>(this.path + 'api/users/save-data', userData)
      .subscribe((_) => {});
  }

  getInquiries(): Observable<GetInquiryData[]> {
    return this.http.get<GetInquiryData[]>(this.path + 'api/inquiries');
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
