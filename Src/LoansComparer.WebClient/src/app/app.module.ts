import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LayoutModule } from '@angular/cdk/layout';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { AuthInterceptorService } from './shared/services/auth/auth-interceptor.service';
import { PersonalDataFormComponent } from './personal-data-form/personal-data-form.component';
import { OfferStatusComponent } from './offer-status/offer-status.component';
import { HomeModule } from './home/home.module';
import { AppLayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [AppComponent, PersonalDataFormComponent, OfferStatusComponent],
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    LayoutModule,
    ScrollingModule,
    AppLayoutModule,
    HomeModule,
    SharedModule,
    AppRoutingModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
