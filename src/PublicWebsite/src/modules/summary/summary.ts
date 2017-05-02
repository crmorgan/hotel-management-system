import {autoinject} from 'aurelia-framework';
import {EventAggregator} from 'aurelia-event-aggregator';
import {Events} from '../../messages/events';

@autoinject()
export class Summary {

	constructor(private messageBus: EventAggregator) {
		
	}

	book() {
		this.messageBus.publish(Events.BookRoom);		
	}
}