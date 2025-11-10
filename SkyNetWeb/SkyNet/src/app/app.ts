import {Component} from '@angular/core';
import {HomeComponent} from './home/home';

@Component({
  selector: 'app-root',
  imports: [HomeComponent],
  template: `
   <div class="main-content">
      <app-home></app-home>
   </div>
  `,
})
export class App {}
