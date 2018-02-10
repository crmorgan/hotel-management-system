import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import shoppingCart from "../../shoppingCart";
import {RatesEvent} from "../rates/rates-event";

const Events = {
  RoomTypeIdsAvailable: 'RoomTypeIdsAvailable'
}

@autoinject()
export class RatesFetcher {
	constructor(private messageBus: EventAggregator, private httpClient: HttpClient) {
		this.messageBus.subscribe(Events.RoomTypeIdsAvailable, response => {
			this.makeApiRequest(response);
		});
	}

	makeApiRequest(response) {
		var idString = '';
		response.map(value => {
			idString += 'ids=' + value + '&';
		});

		let url = 'http://localhost:54520/api/roomTypeRates?' + idString + 'checkin=' + shoppingCart.checkin.toLocaleDateString() + '&checkout=' + shoppingCart.checkout.toLocaleDateString();
		this.httpClient
			.fetch(url)
			.then(response => {
				return response.json();
			})
			.then(data => {
				this.messageBus.publish(RatesEvent.RatesFetched, data);
			});
	}
}
