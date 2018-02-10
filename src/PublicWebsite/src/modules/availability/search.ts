import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import shoppingCart from "../../shoppingCart";
import {AvailabilityEvent} from "../availability/availability-event";
var uniqid = require('uniqid');


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

	findRooms() {
		shoppingCart.checkin = new Date(this.checkin);
		shoppingCart.checkout = new Date(this.checkout);

		let url = 'http://localhost:50673/api/' + 'RoomTypeAvailability?dates.startDate=' + this.checkin + '&dates.endDate=' + this.checkout;

		this.httpClient
			.fetch(url)
			.then(response => {
				return response.json();
			})
			.then(data => {
				this.messageBus.publish(AvailabilityEvent.RoomTypeIdsAvailable, data);
			});
	}
}
