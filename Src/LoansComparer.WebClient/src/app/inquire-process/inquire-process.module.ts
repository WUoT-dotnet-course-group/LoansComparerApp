import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { InquiryProcessRoutingModule } from './inquire-process-routing.module';

import { InquiryFormComponent } from './inquiry-form/inquiry-form.component';
import { ReviewOffersComponent } from './review-offers/review-offers.component';
import { InquirySubmissionFormComponent } from './inquiry-submission-form/inquiry-submission-form.component';
import { SuccessMessageComponent } from './inquiry-submission-form/success-message/success-message.component';
import { InquireProcessComponent } from './inquire-process.component';

@NgModule({
  declarations: [
    InquiryFormComponent,
    ReviewOffersComponent,
    InquirySubmissionFormComponent,
    SuccessMessageComponent,
    InquireProcessComponent,
  ],
  imports: [SharedModule, InquiryProcessRoutingModule],
})
export class InquiryProcessModule {}
