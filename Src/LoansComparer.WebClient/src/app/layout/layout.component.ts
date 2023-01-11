import { Component } from '@angular/core';

@Component({
  selector: 'app-layout',
  template: `
    <div class="stretch" fxLayout="column">
      <div class="header">
        <app-header></app-header>
      </div>

      <div class="content" fxFlex>
        <router-outlet></router-outlet>
      </div>
    </div>

    <div class="footer"></div>
  `,
  styles: [
    `
      .stretch {
        position: absolute;
        top: 0;
        left: 0;
        bottom: 120px;
        width: 100%;
      }

      .header {
        padding: 5px 10px 0px 10px;
      }

      .content {
        padding: 0px 60px;
      }

      .footer {
        position: fixed;
        bottom: 0;
        width: 100%;
        height: 120px;
        margin: 0px;
        background-image: url('../shared/resources/footer.png');
        background-repeat: no-repeat;
        background-size: cover;
        z-index: 0;
      }
    `,
  ],
})
export class LayoutComponent {
  constructor() {}
}
