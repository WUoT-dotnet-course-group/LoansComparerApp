import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { OfferStatusComponent } from './offer-status/offer-status.component';
import { AuthGuard } from './shared/services/auth/auth.guard';
import { PersonalDataFormComponent } from './personal-data-form/personal-data-form.component';

const routes: Routes = [
  {
    path: 'inquire',
    loadChildren: () =>
      import('./inquire-process/inquire-process.module').then(
        (m) => m.InquiryProcessModule
      ),
  },
  { path: 'inquiries/:inquiryId', component: OfferStatusComponent },
  {
    path: 'personal-data',
    component: PersonalDataFormComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
