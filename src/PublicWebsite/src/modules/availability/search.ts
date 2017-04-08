import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {RoomType} from "./RoomType";
import {EventAggregator} from 'aurelia-event-aggregator'

@autoinject()
export class Search {
	name = 'Search component';
	checkin = '8/1/2017';
	checkout = '8/5/2017';

	constructor(private httpClient: HttpClient, private eventAggregator: EventAggregator) {
		this.httpClient.configure(config => {
			config
				.useStandardConfiguration()
				.withBaseUrl('http://localhost:50673/api/')
				.withDefaults({
					headers: {
						'Accept': 'application/json',
					}
				});
		
		});

	}

	checkAvailability() {
		let url = 'RoomTypeAvailability?dates.startDate=' + this.checkin + '&dates.endDate=' + this.checkout;
		this.httpClient
			.fetch(url)
			.then(response => {
				console.log("1", response);
				return response.json();
			})
			.then(data => {
				console.log('2',data);
				this.eventAggregator.publish("RoomTypeIdsAvailable", data);
			});
	}
}