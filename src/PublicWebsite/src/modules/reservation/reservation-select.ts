import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import shoppingCart from "../../shoppingCart";
import {Router} from 'aurelia-router';
var uniqid = require('uniqid');

@autoinject()
export class ReservationSelect {
	@bindable roomTypeId;

	constructor(private httpClient: HttpClient, private router: Router) {
		
	}

	bookRoom() {
    shoppingCart.reservationUuid = uniqid();
    shoppingCart.roomTypeId = this.roomTypeId;

    console.log(`Reservation UUID ${shoppingCart.reservationUuid} added to cart`)

		let url = 'http://localhost:54626/api/reservations';
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
			this.router.navigate("checkout/summary");
		});
	}
}
