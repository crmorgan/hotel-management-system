import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
//import {Search} from '../modules/availability/search';

@autoinject()
export class Branding {
	constructor(private httpClient: HttpClient) {

	}
}
