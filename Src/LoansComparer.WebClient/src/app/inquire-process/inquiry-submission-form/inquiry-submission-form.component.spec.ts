import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InquirySubmissionFormComponent } from './inquiry-submission-form.component';

describe('InquirySubmissionFormComponent', () => {
  let component: InquirySubmissionFormComponent;
  let fixture: ComponentFixture<InquirySubmissionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InquirySubmissionFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InquirySubmissionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
