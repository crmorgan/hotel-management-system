import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

@autoinject()
export class Search {
	name = 'Search component';

	constructor(private httpClient: HttpClient) {
		
	}

	checkAvailability() {
		
	}
}