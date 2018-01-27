import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import shoppingCart from "../../shoppingCart";
var uniqid = require('uniqid');

const Events = {
  RoomTypeIdsAvailable: 'RoomTypeIdsAvailable'
}

@autoinject()
export class Search {
	checkin = '8/1/2018';
	checkout = '8/5/2018';

	constructor(private httpClient: HttpClient, private messageBus: EventAggregator) {
		this.httpClient.configure(config => {
			config
				.useStandardConfiguration()
				.withDefaults({
					headers: {
						'Accept': 'application/json'
					}
				});
		});
	}

	checkAvailability() {
		shoppingCart.checkin = new Date(this.checkin);
		shoppingCart.checkout = new Date(this.checkout);
		shoppingCart.reservationUuid = uniqid();
		shoppingCart.numberOfNights = new Date(shoppingCart.checkout.getTime() - shoppingCart.checkin.getTime()).getUTCDate() -1;

		let url = 'http://localhost:50673/api/' + 'RoomTypeAvailability?dates.startDate=' + this.checkin + '&dates.endDate=' + this.checkout;

		this.httpClient
			.fetch(url)
			.then(response => {
				return response.json();
			})
			.then(data => {
				console.log(data);
				this.messageBus.publish(Events.RoomTypeIdsAvailable, data);
			});
	}
}
