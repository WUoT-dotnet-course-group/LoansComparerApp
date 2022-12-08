import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

@Component({
  selector: 'app-app-description',
  templateUrl: './app-description.component.html',
  styleUrls: ['./app-description.component.less'],
  animations: [
    trigger('toggleLearnMore', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':leave', [animate('0.5s ease-in')]),
      transition(':enter', [animate('0.5s 0.5s ease-in')]),
    ]),
  ],
})
export class AppDescriptionComponent implements OnInit {
  isLearnMore: boolean = true;

  constructor() {}

  ngOnInit(): void {}

  onLearnMoreClick(): void {
    this.isLearnMore = !this.isLearnMore;
  }
}
