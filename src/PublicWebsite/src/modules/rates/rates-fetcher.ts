import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {Events} from '../../messages/events';
import shoppingCart from "../../shoppingCart";

@autoinject()
export class RatesFetcher {
	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe(Events.RoomTypeIdsAvailable, response => {
			this.makeApiRequest(response);
		});
	}

	makeApiRequest(response) {
		var idString = '';
		response.map(value => {
			idString += 'ids=' + value + '&';
		});

		let url = 'http://localhost:54520/api/roomTypeRates?' + idString + 'checkin=' + shoppingCart.checkin + '&checkout=' + shoppingCart.checkout;
		this.apiClient
			.fetch(url)
			.then(response => {
				return response.json();
			})
			.then(data => {
				this.messageBus.publish(Events.RatesFetched, data);
			});
	}
}