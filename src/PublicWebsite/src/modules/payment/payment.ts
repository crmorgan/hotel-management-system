import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {Events} from '../../messages/events';
var uniqid = require('uniqid');
import shoppingCart from "../../shoppingCart";

@autoinject()
export class Payments {
	cardNumber;
	cardExpiration;
	cardType;

	constructor(private httpClient: HttpClient) {
	}

	submit() {
		let url = 'http://localhost:54626//api/payment';
		let body = {
			"paymentUuid": uniqid(),
			"purchaseUuid": shoppingCart.reservationUuid,
			"card": {
				"number": this.cardNumber,
				"typeId": this.cardType,
				"expiration": this.cardExpiration
			}
		}

		this.httpClient.fetch(url, {
			method: 'POST',
			body: json(body)
		})
		.then(response => response.json())
		.then(data => {

		});
	}
}