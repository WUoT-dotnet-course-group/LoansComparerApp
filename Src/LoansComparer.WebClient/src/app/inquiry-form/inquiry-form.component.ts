import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

export interface SelectType {
  id: number;
  name: string;
  description: string;
}

@Component({
  selector: 'app-inquiry-form',
  templateUrl: './inquiry-form.component.html',
  styleUrls: ['./inquiry-form.component.less'],
  animations: [
    trigger('displayForm', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':enter', [animate('0.75s ease-in')]),
    ]),
  ],
})
export class InquiryFormComponent implements OnInit {
  currencySuffix: string = 'zł';
  govIdTypesPlaceholder: SelectType[] = [
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
  jobTypesPlaceholder: SelectType[] = [
    {
      id: 79,
      name: 'Analyst',
      description: 'Analyst',
    },
    {
      id: 80,
      name: 'Producer',
      description: 'Producer',
    },
    {
      id: 81,
      name: 'Technician',
      description: 'Technician',
    },
    {
      id: 84,
      name: 'Manager',
      description: 'Manager',
    },
    {
      id: 87,
      name: 'Liaison',
      description: 'Liaison',
    },
    {
      id: 88,
      name: 'Associate',
      description: 'Associate',
    },
    {
      id: 89,
      name: 'Consultant',
      description: 'Consultant',
    },
    {
      id: 92,
      name: 'Engineer',
      description: 'Engineer',
    },
    {
      id: 93,
      name: 'Strategist',
      description: 'Strategist',
    },
    {
      id: 94,
      name: 'Supervisor',
      description: 'Supervisor',
    },
    {
      id: 95,
      name: 'Executive',
      description: 'Executive',
    },
    {
      id: 96,
      name: 'Planner',
      description: 'Planner',
    },
    {
      id: 97,
      name: 'Developer',
      description: 'Developer',
    },
    {
      id: 98,
      name: 'Officer',
      description: 'Officer',
    },
    {
      id: 99,
      name: 'Architect',
      description: 'Architect',
    },
  ];

  constructor() {}

  ngOnInit(): void {}
}
