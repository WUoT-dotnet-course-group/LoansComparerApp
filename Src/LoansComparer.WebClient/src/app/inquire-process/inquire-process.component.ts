import { Component, OnInit } from '@angular/core';
import { OfferProviderService } from './services/offer-provider.service';

@Component({
  selector: 'app-inquire-process',
  template: `<router-outlet></router-outlet>`,
  providers: [OfferProviderService],
})
export class InquireProcessComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
