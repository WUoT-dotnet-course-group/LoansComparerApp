import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

@Component({
  selector: 'app-description',
  templateUrl: './description.component.html',
  styleUrls: ['./description.component.less'],
  animations: [
    trigger('toggleLearnMore', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':leave', [animate('0.4s ease-in')]),
      transition(':enter', [animate('0.4s 0.4s ease-in')]),
    ]),
  ],
})
export class DescriptionComponent implements OnInit {
  isLearnMore: boolean = true;

  constructor() {}

  ngOnInit(): void {}

  onLearnMoreClick(): void {
    this.isLearnMore = !this.isLearnMore;
  }
}
