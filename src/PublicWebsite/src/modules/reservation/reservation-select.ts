import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import shoppingCart from "../../shoppingCart";
import {Router} from 'aurelia-router';

@autoinject()
export class ReservationSelect {
	@bindable roomTypeId;

	constructor(private httpClient: HttpClient, private messageBus: EventAggregator, private router: Router) {
		
	}

	select() {
		shoppingCart.roomTypeId = this.roomTypeId;
		let url = 'http://localhost:54626//api/reservations';
		let body = {
			"reservationUuid": shoppingCart.reservationUuid,
			"roomTypeId": this.roomTypeId,
			"dates": {
				"startDate": shoppingCart.checkin,
				"endDate": shoppingCart.checkout
			}
		}

		this.httpClient.fetch(url,{
			method: 'POST',
			body: json(body)
		})
		.then(response => response.json())
		.then(data => {
			this.router.navigate("summary");
		});
	}
}