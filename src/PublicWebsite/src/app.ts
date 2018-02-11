import {Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'SOA Hotel';

	config.map([
		{ route: ['', 'branding'], name: 'branding', moduleId: './branding/findRooms', nav: true, title: 'Home' },
		{ route: ['checkout/summary'], name: 'summary', moduleId: './branding/checkout/summary' },
		{ route: ['checkout/confirmation'], name: 'confirmation', moduleId: './branding/checkout/confirmation' }
    ]);

    this.router = router;
  }
}
