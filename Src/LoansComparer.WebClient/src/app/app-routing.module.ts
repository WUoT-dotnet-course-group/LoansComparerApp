import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { InquiryFormComponent } from './inquiry-form/inquiry-form.component';
import { ReviewOffersComponent } from './review-offers/review-offers.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'inquire', component: InquiryFormComponent },
  { path: 'offers', component: ReviewOffersComponent },
  { path: '**', redirectTo: '/home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
