import { Component, OnInit } from '@angular/core';
import {
  ControlContainer,
  FormBuilder,
  FormControl,
  FormGroup,
  FormGroupDirective,
  Validators,
} from '@angular/forms';
import { ErrorMessage } from '../resources/error-message';
import {
  DictionaryDTO,
  LoansComparerService,
} from '../services/loans-comparer/loans-comparer.service';

@Component({
  selector: 'app-personal-data',
  templateUrl: './personal-data.component.html',
  styleUrls: ['./personal-data.component.less'],
  viewProviders: [
    { provide: ControlContainer, useExisting: FormGroupDirective },
  ],
})
export class PersonalDataComponent implements OnInit {
  parentForm!: FormGroup;
  personalDataForm!: FormGroup;

  governmentIdTypes: DictionaryDTO[] = [];
  jobTypes: DictionaryDTO[] = [];

  dateNow!: Date;
  dateEighteenYearsBefore!: Date;

  requiredFieldError: string = ErrorMessage.requiredField;
  invalidFirstNameError: string = ErrorMessage.invalidFirstName;
  invalidLastNameError: string = ErrorMessage.invalidLastName;
  invalidBirthDateError: string = ErrorMessage.invalidBirthDate;
  invalidJobStartError: string = ErrorMessage.invalidJobStart;
  invalidJobEndError: string = ErrorMessage.invalidJobEnd;

  constructor(
    private parent: FormGroupDirective,
    private formBuilder: FormBuilder,
    private loansComparerService: LoansComparerService
  ) {}

  ngOnInit(): void {
    this.parentForm = this.parent.form;

    this.personalDataForm = this.formBuilder.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]+'),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]+'),
      ]),
      birthDate: new FormControl(null, Validators.required),
      governmentDocument: new FormGroup({
        governmentIdType: new FormControl(null, Validators.required),
        governmentId: new FormControl(null, Validators.required),
      }),
      jobDetails: new FormGroup({
        jobType: new FormControl(null, Validators.required),
        jobStartDate: new FormControl(null, Validators.required),
        jobEndDate: new FormControl(null, Validators.required),
      }),
    });
    this.parentForm.addControl('personalData', this.personalDataForm);

    this.dateNow = new Date(Date.now());
    this.dateEighteenYearsBefore = new Date(Date.now());
    this.dateEighteenYearsBefore.setFullYear(this.dateNow.getFullYear() - 18);

    this.loansComparerService.getGovernmentIdTypes().subscribe((response) => {
      this.governmentIdTypes = response;
    });

    this.loansComparerService.getJobTypes().subscribe((response) => {
      this.jobTypes = response;
    });
  }

  get jobStartDate(): Date | null {
    return this.personalDataForm.get('jobDetails.jobStartDate')?.value;
  }

  get jobEndDate(): Date | null {
    return this.personalDataForm.get('jobDetails.jobEndDate')?.value;
  }
}
