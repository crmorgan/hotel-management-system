import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {Events} from '../../messages/events';

@autoinject()
export class Rates {
	@bindable roomTypeId;
	rate;

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe(Events.RatesFetched, response => {
			this.rate = this.getRate(response);
		});
	}
	getRate(response) {
		return response.filter(match => {
			return this.roomTypeId === match.RoomTypeId;
		})[0];
	}
}