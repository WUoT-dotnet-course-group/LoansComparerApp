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
    trigger('animation', [
      state('*', style({ opacity: 1 })),
      state('void', style({ opacity: 0 })),
      transition(':leave', [animate('0.4s ease-in')]),
      transition(':enter', [animate('0.4s 0.4s ease-in')]),
    ]),
  ],
})
export class HomeComponent implements OnInit {
  currentInquiresMade: number = 102;

  constructor() {}

  ngOnInit(): void {}
}
