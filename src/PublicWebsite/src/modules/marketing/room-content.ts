import {EventAggregator} from 'aurelia-event-aggregator'
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import './room-content.css'

export const Events = {
  RoomContentFetched: 'RoomContentFetched'
}

@autoinject()
export class RoomContent {
	description;
	imageUrl;
	@bindable roomTypeId;

	constructor(private messageBus: EventAggregator, private apiClient: HttpClient) {
		this.messageBus.subscribe(Events.RoomContentFetched, data => {
			let content = this.getRoomContent(data);
			this.description = content.Description;
			this.imageUrl = content.ImageUrl;
			
		});
	}

	getRoomContent(data) {
		return data.filter(match => {
			return this.roomTypeId === match.RoomTypeId;
		})[0];
	}
}
