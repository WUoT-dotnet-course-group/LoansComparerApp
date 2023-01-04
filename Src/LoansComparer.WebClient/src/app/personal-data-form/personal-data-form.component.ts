import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { FormGroup } from '@angular/forms';
import {
  LoansComparerService,
  PersonalDataDTO,
} from '../shared/services/loans-comparer/loans-comparer.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-personal-data-form',
  templateUrl: './personal-data-form.component.html',
  styleUrls: ['./personal-data-form.component.less'],
  animations: [
    trigger('displayForm', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':enter', [animate('0.75s ease-in')]),
      transition(':leave', [animate('0.75s ease-in')]),
    ]),
  ],
})
export class PersonalDataFormComponent implements OnInit {
  form: FormGroup = new FormGroup({});

  constructor(
    private loansComparerService: LoansComparerService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  onFormSubmit(): void {
    // TODO: do mapping on backend
    let personalData = <PersonalDataDTO>this.form.value.personalData;
    personalData.jobType = this.form.value.personalData.jobType.typeId;
    personalData.governmentIdType =
      this.form.value.personalData.governmentIdType.typeId;

    this.loansComparerService.saveUserData(personalData);

    this.router.navigateByUrl('/home');
  }
}
