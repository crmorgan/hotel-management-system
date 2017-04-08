import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

@autoinject()
export class Rates {
	name = 'Room content component';

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe("RoomTypeIdsAvailable", response => {
			this.makeApiRequest(response);
		});

//		this.apiClient.configure(config => {
//			config
//				.useStandardConfiguration()
//				.withBaseUrl('http://localhost:54520/api/')
//				.withDefaults({
//					headers: {
//						'Accept': 'application/json',
//					}
//				});
//
//		});
	}

	makeApiRequest(response) {
		console.log('rates', response);

		var idString = '';

		response.map(function(value) {
			idString += 'ids=' + value + '&';
		});

		console.log(idString);

//		let url = 'roomTypeRates?ids=1&ids=3&ids=2&ids=4';
//		this.apiClient
//			.fetch(url)
//			.then(response => {
//				console.log("1", response);
//				return response.json();
//			})
//			.then(data => {
//				console.log('2', data);
//				this.messageBus.publish("RoomTypeIdsAvailable", data);
//			});
		
	}
}