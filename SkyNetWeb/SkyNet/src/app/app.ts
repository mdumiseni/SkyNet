import {Component} from '@angular/core';
import {HomeComponent} from './home/home';

@Component({
  selector: 'app-root',
  imports: [HomeComponent],
  template: `
   <main>
    <header class="brand-name">
      <img class="brand-logo" src="https://skynetworldwide.com/za/wp-content/uploads/2023/02/skynet-logo.png.webp" alt="logo" aria-hidden="true">
    </header>
    <section class="content">
      <app-home></app-home>
    </section>
  </main>
  `,
})
export class App {}
