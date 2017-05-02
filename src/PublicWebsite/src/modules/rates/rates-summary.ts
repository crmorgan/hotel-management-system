import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {Events} from '../../messages/events';
import shoppingCart from "../../shoppingCart";

@autoinject()
export class RatesSummary {
	nightlyRate;
	totalRate;
	numberOfNights;

	constructor(private messageBus: EventAggregator, private httpClient: HttpClient) {
		this.numberOfNights = shoppingCart.numberOfNights;
		this.getRate();
	}

	activate() {
		
	}
	getRate() {
		let url = 'http://localhost:54520/api/roomTypeRates?' + 'ids=' + shoppingCart.roomTypeId + '&checkin=' + shoppingCart.checkin.toLocaleDateString() + '&checkout=' + shoppingCart.checkout.toLocaleDateString();

		this.httpClient.fetch(url)
		.then(response => response.json())
		.then(data => {
			let rate = data[0];

			this.messageBus.publish(Events.RatesSummaryFetched, rate);

			this.nightlyRate = rate.Amount;
			this.totalRate = rate.Amount * shoppingCart.numberOfNights;
			this.numberOfNights = shoppingCart.numberOfNights;
		});
	}
}