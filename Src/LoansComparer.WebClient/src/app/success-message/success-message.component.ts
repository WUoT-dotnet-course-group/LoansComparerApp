import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-success-message',
  templateUrl: './success-message.component.html',
  styleUrls: ['./success-message.component.css'],
})
export class SuccessMessageComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<SuccessMessageComponent>) {}

  ngOnInit(): void {}
}
