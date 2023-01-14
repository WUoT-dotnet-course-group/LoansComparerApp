import { Injectable } from '@angular/core';
import { ReviewOffer } from './offer-provider.service';

export interface PersonalData {
  firstName: string;
  lastName: string;
  governmentIdType: string;
  governmentId: string;
}

export interface InquiryDetails {
  id: string;
  loanValue: number;
  loanPeriod: number;
  firstName: string;
  lastName: string;
  governmentIdType: string;
  governmentId: string;
  documentLink: string | null;
  percentage: number;
  monthlyInstallment: number;
  bankName: string;
}

@Injectable()
export class InquireDataStorageService {
  inquiryId: string | null = null;
  personalData: PersonalData | null = null;
  selectedOffer: ReviewOffer | null = null;

  constructor() {}

  get hasAllData(): boolean {
    return !!this.inquiryId && !!this.personalData && !!this.selectedOffer;
  }

  getInquiryDetails(): InquiryDetails {
    return {
      id: this.inquiryId!,
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
