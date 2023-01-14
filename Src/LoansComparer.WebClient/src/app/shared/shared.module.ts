import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';

import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { PersonalDataComponent } from './components/personal-data/personal-data.component';
import { ReturnButtonComponent } from './components/return-button/return-button.component';

@NgModule({
  declarations: [
    PersonalDataComponent,
    LoadingSpinnerComponent,
    ReturnButtonComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MaterialModule,
  ],
  exports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MaterialModule,
    PersonalDataComponent,
    LoadingSpinnerComponent,
    ReturnButtonComponent,
  ],
})
export class SharedModule {}
