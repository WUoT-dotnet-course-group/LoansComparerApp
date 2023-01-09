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
import { AuthService } from '../services/auth/auth.service';
import {
  DictionaryDTO,
  LoansComparerService,
  PersonalDataDTO,
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
    private loansComparerService: LoansComparerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.initPersonalDataForm();

    this.setDateConstants();

    if (this.authService.isAuthenticated) {
      this.fetchPersonalData();
    }

    this.fetchDropdownOptions();
  }

  get jobStartDate(): Date | null {
    return this.personalDataForm.get('jobDetails.jobStartDate')?.value;
  }

  get jobEndDate(): Date | null {
    return this.personalDataForm.get('jobDetails.jobEndDate')?.value;
  }

  private initPersonalDataForm(): void {
    this.parentForm = this.parent.form;

    this.personalDataForm = this.formBuilder.group({
      firstName: new FormControl<string>('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]+'),
      ]),
      lastName: new FormControl<string>('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]+'),
      ]),
      birthDate: new FormControl<Date | null>(null, Validators.required),
      governmentDocument: new FormGroup({
        governmentIdType: new FormControl<DictionaryDTO | null>(
          null,
          Validators.required
        ),
        governmentId: new FormControl<string>('', Validators.required),
      }),
      jobDetails: new FormGroup({
        jobType: new FormControl<DictionaryDTO | null>(
          null,
          Validators.required
        ),
        jobStartDate: new FormControl<Date | null>(null, Validators.required),
        jobEndDate: new FormControl<Date | null>(null, Validators.required),
      }),
    });
    this.parentForm.addControl('personalData', this.personalDataForm);
  }

  private setDateConstants(): void {
    this.dateNow = new Date(Date.now());
    this.dateEighteenYearsBefore = new Date(Date.now());
    this.dateEighteenYearsBefore.setFullYear(this.dateNow.getFullYear() - 18);
  }

  private fetchPersonalData(): void {
    this.loansComparerService
      .getUserData()
      .subscribe((data: PersonalDataDTO) => {
        this.personalDataForm.setValue(data);
        this.setDropdownValues(data); // dropdown values need to be set separately
      });
  }

  private fetchDropdownOptions(): void {
    this.loansComparerService.getGovernmentIdTypes().subscribe((response) => {
      this.governmentIdTypes = response;
    });

    this.loansComparerService.getJobTypes().subscribe((response) => {
      this.jobTypes = response;
    });
  }

  private setDropdownValues(data: PersonalDataDTO) {
    const govDocValue = this.governmentIdTypes.find(
      (t) => t.typeId == data.governmentDocument.governmentIdType.typeId
    );
    this.personalDataForm
      .get('governmentDocument.governmentIdType')
      ?.setValue(govDocValue);

    const jobValue = this.jobTypes.find(
      (t) => t.typeId == data.jobDetails.jobType.typeId
    );
    this.personalDataForm.get('jobDetails.jobType')?.setValue(jobValue);
  }
}
