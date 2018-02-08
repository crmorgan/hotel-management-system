import {Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Acme Hotel';

	config.map([
		{ route: ['', 'branding'], name: 'branding', moduleId: './branding/findRooms', nav: true, title: 'Home' },
		{ route: ['summary'], name: 'summary', moduleId: './modules/summary/summary' },
		{ route: ['confirmation'], name: 'confirmation', moduleId: './branding/confirmation' }
    ]);

    this.router = router;
  }
}
