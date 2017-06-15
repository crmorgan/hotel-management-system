import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator'
import './branding.css'

export const Events = {
  RoomTypeIdsAvailable: 'RoomTypeIdsAvailable'
}

@autoinject()
export class Branding {
	roomTypeIds;

	constructor(private httpClient: HttpClient, private messageBus: EventAggregator) {
		this.messageBus.subscribe(Events.RoomTypeIdsAvailable, response => {
			this.roomTypeIds = response;
		});
	}
}
