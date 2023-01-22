import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import {
  CreateInquiryDTO,
  CreateInquiryResponseDTO,
  LoansComparerService,
  PersonalDataDTO,
} from '../../shared/services/loans-comparer/loans-comparer.service';
import { ErrorMessage } from '../../shared/resources/error-message';
import { OfferProviderService } from '../services/offer-provider.service';
import { AuthService } from '../../shared/services/auth/auth.service';
import {
  InquireDataStorageService,
  PersonalData,
} from '../services/inquire-data-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inquiry-form',
  templateUrl: './inquiry-form.component.html',
  styleUrls: ['./inquiry-form.component.less'],
  animations: [
    trigger('displayForm', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':enter', [animate('0.75s ease-in')]),
      transition(':leave', [animate('0.75s ease-in')]),
    ]),
  ],
})
export class InquiryFormComponent implements OnInit {
  inquiryForm!: FormGroup;
  personalDataForm!: FormGroup;

  requiredFieldError: string = ErrorMessage.requiredField;

  constructor(
    protected loansComparerService: LoansComparerService,
    protected offerProviderService: OfferProviderService,
    protected inquireDataStorageService: InquireDataStorageService,
    protected authService: AuthService,
    protected router: Router
  ) {}

  ngOnInit(): void {
    this.inquiryForm = new FormGroup({
      loanValue: new FormControl(null, Validators.required),
      numberOfInstallments: new FormControl(null, Validators.required),
    });
  }

  onReturn(event: any): void {
    this.router.navigate(['/home']);
  }

  onFormSubmit(): void {
    this.loansComparerService
      .createInquiry(<CreateInquiryDTO>this.inquiryForm.value)
      .subscribe((response: CreateInquiryResponseDTO) => {
        this.offerProviderService.initOffers(response);
      });

    const personalData = <PersonalDataDTO>(
      (<unknown>this.inquiryForm.get('personalData')!.value)
    );
    this.inquireDataStorageService.personalData = <PersonalData>{
      firstName: personalData.firstName,
      lastName: personalData.lastName,
      governmentId: personalData.governmentDocument.governmentId,
      governmentIdType: personalData.governmentDocument.governmentIdType.name,
    };
  }
}
