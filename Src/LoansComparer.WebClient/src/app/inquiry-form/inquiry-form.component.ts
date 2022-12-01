import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-inquiry-form',
  templateUrl: './inquiry-form.component.html',
  styleUrls: ['./inquiry-form.component.less'],
})
export class InquiryFormComponent implements OnInit {
  currencySuffix: string = 'zł';

  constructor() {}

  ngOnInit(): void {}
}
