import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InquireProcessComponent } from './inquire-process.component';

describe('InquireProcessComponent', () => {
  let component: InquireProcessComponent;
  let fixture: ComponentFixture<InquireProcessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InquireProcessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InquireProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
