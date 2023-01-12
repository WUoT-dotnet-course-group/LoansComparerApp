import { Component, OnInit } from '@angular/core';
import {
  ControlContainer,
  FormControl,
  FormGroup,
  FormGroupDirective,
  Validators,
} from '@angular/forms';
import { LoansComparerService } from 'src/app/shared/services/loans-comparer/loans-comparer.service';

@Component({
  selector: 'app-document-upload',
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.less'],
  viewProviders: [
    { provide: ControlContainer, useExisting: FormGroupDirective },
  ],
})
export class DocumentUploadComponent implements OnInit {
  parentForm!: FormGroup;

  constructor(
    private parent: FormGroupDirective,
    private loansComparerService: LoansComparerService
  ) {}

  ngOnInit(): void {
    this.parentForm = this.parent.form;
    this.parentForm.addControl(
      'document',
      new FormControl<File | null>(null, [Validators.required])
    );
  }

  fileName = '';
  file: File | null = null;

  onFileSelected(event: any) {
    this.file = event.target.files[0];
    if (this.file) {
      this.parentForm.patchValue({ document: this.file });
      this.fileName = this.file.name;
    }
  }
}
