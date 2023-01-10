import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InquireProcessComponent } from './inquire-process.component';
import { InquiryFormComponent } from './inquiry-form/inquiry-form.component';
import { InquirySubmissionFormComponent } from './inquiry-submission-form/inquiry-submission-form.component';
import { ReviewOffersComponent } from './review-offers/review-offers.component';

const routes: Routes = [
  {
    path: '',
    component: InquireProcessComponent,
    children: [
      {
        path: '',
        component: InquiryFormComponent,
        pathMatch: 'full',
      },
      {
        path: 'offers',
        component: ReviewOffersComponent,
      },
      {
        path: 'submit/:inquiryId',
        component: InquirySubmissionFormComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InquiryProcessRoutingModule {}
