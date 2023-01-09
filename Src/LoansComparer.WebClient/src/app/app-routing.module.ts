import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DescriptionComponent } from './description/description.component';
import { HomeComponent } from './home/home.component';
import { InquiryFormComponent } from './inquiry-form/inquiry-form.component';
import { InquirySubmissionFormComponent } from './inquiry-submission-form/inquiry-submission-form.component';
import { InquiryHistoryComponent } from './inquiry-history/inquiry-history.component';
import { OfferStatusComponent } from './offer-status/offer-status.component';
import { ReviewOffersComponent } from './review-offers/review-offers.component';
import { AuthGuard } from './shared/services/auth/auth.guard';
import { PersonalDataFormComponent } from './personal-data-form/personal-data-form.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    children: [
      {
        path: 'signed-in',
        component: InquiryHistoryComponent,
        canActivate: [AuthGuard],
      },
      { path: '', component: DescriptionComponent },
    ],
  },
  {
    path: 'personal-data',
    component: PersonalDataFormComponent,
    canActivate: [AuthGuard],
  },
  { path: 'inquire', component: InquiryFormComponent },
  {
    path: 'offers',
    component: ReviewOffersComponent,
  },
  // { path: 'offers/:bankId/:offerId', component: OfferStatusComponent },
  { path: 'offers/status', component: OfferStatusComponent },
  {
    path: 'inquire/submit/:inquiryId',
    component: InquirySubmissionFormComponent,
  },
  { path: '**', redirectTo: '/home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
