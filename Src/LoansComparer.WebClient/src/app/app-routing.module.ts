import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppDescriptionComponent } from './app-description/app-description.component';
import { HomeComponent } from './home/home.component';
import { InquiryFormComponent } from './inquiry-form/inquiry-form.component';
import { InquiryHistoryComponent } from './inquiry-history/inquiry-history.component';
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
      { path: '', component: AppDescriptionComponent },
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
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: '/home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
