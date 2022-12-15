import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InquiryHistoryComponent } from './inquiry-history.component';

describe('InquiryHistoryComponent', () => {
  let component: InquiryHistoryComponent;
  let fixture: ComponentFixture<InquiryHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InquiryHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InquiryHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
