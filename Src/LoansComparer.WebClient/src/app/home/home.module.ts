import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';

import { DescriptionComponent } from './description/description.component';
import { HomeComponent } from './home.component';
import { InquiryHistoryComponent } from './inquiry-history/inquiry-history.component';

@NgModule({
  declarations: [HomeComponent, DescriptionComponent, InquiryHistoryComponent],
  imports: [SharedModule, HomeRoutingModule],
  exports: [HomeComponent],
})
export class HomeModule {}
