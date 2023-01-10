import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../shared/services/auth/auth.guard';
import { DescriptionComponent } from './description/description.component';
import { HomeComponent } from './home.component';
import { InquiryHistoryComponent } from './inquiry-history/inquiry-history.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: 'home',
        component: InquiryHistoryComponent,
        canActivate: [AuthGuard],
      },
      { path: '', component: DescriptionComponent, pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
