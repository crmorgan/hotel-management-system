import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

@autoinject()
export class RoomContent {
	name = 'Room content component';

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe("RoomTypeIdsAvailable", response => {
			this.makeApiRequest(response);
		});
	}

	makeApiRequest(response) {
		console.log('marketing', response);
	}
}