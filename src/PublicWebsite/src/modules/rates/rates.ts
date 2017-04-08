import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

@autoinject()
export class Rates {
	name = 'Room content component';

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe("RoomTypeIdsAvailable", response => {
			console.info("sub to RoomTypeIdsAvailable");
			this.makeApiRequest(response);
		});
	}

	makeApiRequest(response) {
		console.log('rates', response);

//		var idString = 'http://localhost:54520/api/roomTypeRates?';
//
//		response.map(function(value) {
//			idString += 'ids=' + value + '&';
//		});

//		console.log(idString);

		console.log("get rates: http");
		let url = 'http://localhost:54520/api/roomTypeRates?ids=1&ids=3&ids=2&ids=4&checkin=8/1/2017&checkout=8/6/2017';
		this.apiClient
			.fetch(url)
			.then(response => {
				console.log("1: rates", response);
				return response.json();
			})
			.then(data => {
				console.log('2: rates', data);

			});

	}
}