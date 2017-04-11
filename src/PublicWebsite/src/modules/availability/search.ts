import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import {Events} from '../../messages/events';
import {globalVars} from "../../global";

@autoinject()
export class Search {
	checkin = '8/1/2017';
	checkout = '8/5/2017';

	constructor(private httpClient: HttpClient, private eventAggregator: EventAggregator) {
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
		globalVars.checkin = this.checkin;
		globalVars.checkout = this.checkout;
		let url = 'http://localhost:50673/api/' + 'RoomTypeAvailability?dates.startDate=' + this.checkin + '&dates.endDate=' + this.checkout;

		this.httpClient
			.fetch(url)
			.then(response => {
				return response.json();
			})
			.then(data => {
				this.eventAggregator.publish(Events.RoomTypeIdsAvailable, data);
			});
	}
}