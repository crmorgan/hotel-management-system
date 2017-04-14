import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {Events} from '../../messages/events';

@autoinject()
export class RoomContent {
	content;

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe(Events.RoomTypeIdsAvailable, response => {
			this.makeApiRequest(response);
		});
	}

	makeApiRequest(response) {
		var idString = '';
		response.map((value, i) => {
			idString += 'ids=' + value;
			if (response.lastIndexOf(response.length) !== i) {
				idString += '&';
			}
		});

		let url = 'http://localhost:54831/api/collateral/roomtypes?' + idString;
		this.apiClient
			.fetch(url)
			.then(response => {
				return response.json();
			})
			.then(data => {
				this.content = data;
			});
	}
}