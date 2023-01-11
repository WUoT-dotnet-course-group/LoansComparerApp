import { Component, OnInit } from '@angular/core';
import { LoansComparerService } from 'src/app/shared/services/loans-comparer/loans-comparer.service';

@Component({
  selector: 'app-document-upload',
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.less'],
})
export class DocumentUploadComponent implements OnInit {
  constructor(private loansComparerService: LoansComparerService) {}

  ngOnInit(): void {}

  fileName = '';
  file: File | null = null;

  onFileSelected(event: any) {
    this.file = event.target.files[0];
    if (this.file) {
      this.fileName = this.file.name;
    }
  }
}
