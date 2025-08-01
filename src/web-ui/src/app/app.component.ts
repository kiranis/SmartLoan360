import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="container">
      <h1>{{title}}</h1>
      <router-outlet></router-outlet>
    </div>
  `,
  styles: [`
    .container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 20px;
    }
    h1 {
      color: #333;
      text-align: center;
      margin-bottom: 30px;
    }
  `]
})
export class AppComponent {
  title = 'SmartLoan360';
}
