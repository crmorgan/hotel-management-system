import {Aurelia} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Hotel Management System';
    //config.map([
    //  { route: ['', 'welcome'], name: 'welcome',      moduleId: './welcome',      nav: true, title: 'Welcome' },
    //  { route: 'users',         name: 'users',        moduleId: './users',        nav: true, title: 'Github Users' },
    //  { route: 'child-router',  name: 'child-router', moduleId: './child-router', nav: true, title: 'Child Router' }
    //]);

	config.map([
		{ route: ['', 'branding'], name: 'branding', moduleId: './branding/branding', nav: true, title: 'Home' }
    ]);

    this.router = router;
  }
}
