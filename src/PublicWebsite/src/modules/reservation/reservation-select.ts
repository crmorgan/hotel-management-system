import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import shoppingCart from "../../shoppingCart";

@autoinject()
export class ReservationSelect {
	@bindable roomTypeId;

	constructor(private httpClient: HttpClient) {
		
	}

	select() {
		let url = 'http://localhost:54626//api/reservations';
		let body = {
			"roomTypeId": this.roomTypeId,
			"dates": {
				"startDate": shoppingCart.checkin,
				"endDate": shoppingCart.checkout
			}
//			"reservationUuid": shoppingCart.reservationUuid
		}

		this.httpClient.fetch(url,{
			method: 'POST',
			body: json(body)
		})
		.then(response => response.json())
		.then(data => {
			alert('Post success');
		});
	}
}