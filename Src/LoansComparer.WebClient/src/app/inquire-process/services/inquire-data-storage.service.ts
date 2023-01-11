import { Injectable } from '@angular/core';
import { ReviewOffer } from './offer-provider.service';

export interface PersonalData {
  firstName: string;
  lastName: string;
  governmentIdType: string;
  governmentId: string;
}

export interface InquiryDetails {
  loanValue: number;
  loanPeriod: number;
  firstName: string;
  lastName: string;
  governmentIdType: string;
  governmentId: string;
  documentLink: string;
  percentage: number;
  monthlyInstallment: number;
  bankName: string;
}

@Injectable()
export class InquireDataStorageService {
  personalData: PersonalData | null = null;
  selectedOffer: ReviewOffer | null = null;

  constructor() {}

  get hasAllData(): boolean {
    return !!this.personalData && !!this.selectedOffer;
  }

  getInquiryDetails(): InquiryDetails {
    return {
      bankName: this.selectedOffer!.bankName,
      firstName: this.personalData!.firstName,
      lastName: this.personalData!.lastName,
      governmentId: this.personalData!.governmentId,
      governmentIdType: this.personalData!.governmentIdType,
      loanPeriod: this.selectedOffer!.loanPeriod,
      loanValue: this.selectedOffer!.loanValue,
      monthlyInstallment: this.selectedOffer!.monthlyInstallment,
      documentLink: this.selectedOffer!.documentLink,
      percentage: this.selectedOffer!.percentage,
    };
  }
}
