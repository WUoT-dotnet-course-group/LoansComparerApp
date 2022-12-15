import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { InquiryFormComponent } from './inquiry-form/inquiry-form.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'create-inquiry', component: InquiryFormComponent },
  { path: '**', redirectTo: '/home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
