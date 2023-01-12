import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-return-button',
  template: `
    <button
      mat-mini-fab
      type="button"
      class="app-secondary"
      fxLayoutAlign="center center"
      (click)="this.click.emit($event)"
    >
      <mat-icon>arrow_back</mat-icon>
    </button>
  `,
  styles: [
    `
      button {
        color: rgb(119, 46, 158);
        box-shadow: 0px 3px 5px -1px rgba(0 0 0 / 10%),
          0px 3px 5px -1px rgba(0 0 0 / 8%), 0px 3px 5px -1px rgba(0 0 0 / 6%);
      }
    `,
  ],
})
export class ReturnButtonComponent {
  @Output() click = new EventEmitter<any>();

  constructor() {}
}
