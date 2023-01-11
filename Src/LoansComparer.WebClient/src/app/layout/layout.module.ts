import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { HeaderComponent } from './header/header.component';
import { LayoutComponent } from './layout.component';
import { SignInGoogleComponent } from './header/sign-in-google/sign-in-google.component';
import { SignOutComponent } from './header/sign-out/sign-out.component';

@NgModule({
  declarations: [
    LayoutComponent,
    HeaderComponent,
    SignInGoogleComponent,
    SignOutComponent,
  ],
  imports: [SharedModule],
  exports: [LayoutComponent],
})
export class AppLayoutModule {}
