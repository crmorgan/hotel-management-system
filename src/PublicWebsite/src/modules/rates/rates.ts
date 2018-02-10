import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {RatesEvent} from "../rates/rates-event";

@autoinject()
export class Rates {
	@bindable roomTypeId;
	rate;

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe(RatesEvent.RatesFetched, response => {
			this.rate = this.getRate(response);
		});
	}

	getRate(response) {
		return response.filter(match => {
			return this.roomTypeId === match.roomTypeId;
		})[0];
	}
}
