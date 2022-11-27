import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],
  animations: [
    trigger('toggleLearnMore', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition('* => void', [animate('0.5s ease-in')]),
      transition('void => *', [animate('0.5s 0.5s ease-in')]),
    ]),
  ],
})
export class HomeComponent implements OnInit {
  isLearnMore: boolean = true;

  constructor() {}

  ngOnInit(): void {}

  onLearnMoreClick(): void {
    this.isLearnMore = !this.isLearnMore;
  }
}
