import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import {Events} from '../../messages/events';
import shoppingCart from "../../shoppingCart";
import {Router} from 'aurelia-router';

@autoinject()
export class ReservationAmount {
	amount;

	constructor(private httpClient: HttpClient, private messageBus: EventAggregator, private router: Router) {
		this.messageBus.subscribe(Events.RatesSummaryFetched, message => {
			this.amount = message.ammount;
		});
	}

	submit() {
		let url = 'http://localhost:54626//api/reservations';
		let body = {
			"reservationUuid": shoppingCart.reservationUuid,
			"ammount": this.amount
		}

		this.httpClient.fetch(url,{
			method: 'POST',
			body: json(body)
		})
		.then(response => response.json())
		.then(data => {

		});
	}
}