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

  inquiryForm!: FormGroup;
  personalDataForm!: FormGroup;

  requiredFieldError: string = ErrorMessage.requiredField;

  constructor(
    protected loansComparerService: LoansComparerService,
    protected router: Router
  ) {}

  ngOnInit(): void {
    this.inquiryForm = new FormGroup({
      loanValue: new FormControl(null, Validators.required),
      numberOfInstallments: new FormControl(null, Validators.required),
    });
  }

  onFormSubmit(): void {
    this.loansComparerService.createInquiry(
      <AddInquiryDTO>this.inquiryForm.value
    );

    this.router.navigateByUrl('/offers');
  }
}
