import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { LoansComparerService } from '../shared/services/loans-comparer/loans-comparer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],
  animations: [
    trigger('animation', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':leave', [animate('0.4s ease-in')]),
      transition(':enter', [animate('0.4s 0.4s ease-in')]),
    ]),
  ],
})
export class HomeComponent implements OnInit {
  currentInquiresMade!: number;

  constructor(private loansComparerService: LoansComparerService) {}

  ngOnInit(): void {
    this.loansComparerService
      .getInquiriesAmount()
      .subscribe((amount: number) => (this.currentInquiresMade = amount));
  }
}
