import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { Router } from '@angular/router';
import {
  AddInquiryDTO,
  DictionaryDTO,
  LoansComparerService,
} from '../shared/services/loans-comparer/loans-comparer.service';
import { ErrorMessage } from '../shared/resources/error-message';

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
  currencySuffix: string = 'zł';
  govIdTypesPlaceholder: DictionaryDTO[] = [
    {
      id: 1,
      name: 'Driver License',
      description: 'Driver License',
    },
    {
      id: 2,
      name: 'Passport',
      description: 'Passport',
    },
    {
      id: 3,
      name: 'Government Id',
      description: 'Government Id',
    },
  ];
  jobTypesPlaceholder: DictionaryDTO[] = [];

  inquiryForm!: FormGroup;

  dateNow!: Date;
  dateEighteenYearsBefore!: Date;

  invalidFirstNameError: string = ErrorMessage.invalidFirstName;
  invalidLastNameError: string = ErrorMessage.invalidLastName;
  invalidBirthDateError: string = ErrorMessage.invalidBirthDate;
  requiredFieldError: string = ErrorMessage.requiredField;
  invalidJobStartError: string = ErrorMessage.invalidJobStart;
  invalidJobEndError: string = ErrorMessage.invalidJobEnd;

  constructor(
    protected loansComparerService: LoansComparerService,
    protected router: Router
  ) {}

  ngOnInit(): void {
    this.dateNow = new Date(Date.now());

    this.dateEighteenYearsBefore = new Date(Date.now());
    this.dateEighteenYearsBefore.setFullYear(this.dateNow.getFullYear() - 18);

    this.inquiryForm = new FormGroup({
      personalData: new FormGroup({
        firstName: new FormControl('', [
          Validators.required,
          Validators.pattern('[a-zA-Z]+'),
        ]),
        lastName: new FormControl('', [
          Validators.required,
          Validators.pattern('[a-zA-Z]+'),
        ]),
        birthDate: new FormControl(null, Validators.required),
      }),
      loanValue: new FormControl(null, Validators.required),
      installmentsNumber: new FormControl(null, Validators.required),
      governmentIdType: new FormControl(null, Validators.required),
      governmentId: new FormControl(null, Validators.required),
      jobType: new FormControl(null, Validators.required),
      jobStartDate: new FormControl(null, Validators.required),
      jobEndDate: new FormControl(null, Validators.required),
    });

    this.loansComparerService.getJobTypes().subscribe((response) => {
      this.jobTypesPlaceholder = response;
    });
  }

  onFormSubmit(): void {
    this.loansComparerService.createInquiry(<AddInquiryDTO>{
      loanValue: this.inquiryForm.value.loanValue,
      numberOfInstallments: this.inquiryForm.value.installmentsNumber,
      personalData: this.inquiryForm.value.personalData,
      governmentDocument: {
        typeId: this.inquiryForm.value.governmentIdType.id,
        name: this.inquiryForm.value.governmentIdType.name,
        description: this.inquiryForm.value.governmentIdType.description,
        number: this.inquiryForm.value.governmentId,
      },
      jobDetails: {
        typeId: this.inquiryForm.value.jobType.id,
        name: this.inquiryForm.value.jobType.name,
        description: this.inquiryForm.value.jobType.description,
        jobStartDate: this.inquiryForm.value.jobStartDate,
        jobEndDate: this.inquiryForm.value.jobEndDate,
      },
    });

    this.router.navigateByUrl('/offers');
  }

  get jobStartDate(): Date {
    return this.inquiryForm.controls['jobStartDate'].value;
  }

  get jobEndDate(): Date {
    return this.inquiryForm.controls['jobEndDate'].value;
  }
}
