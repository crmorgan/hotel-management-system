import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import {Events} from '../../messages/events';
var uniqid = require('uniqid');
import shoppingCart from "../../shoppingCart";

@autoinject()
export class Payments {
	cardNumber = 371449635398431;
	cardExpiration = '4/20';
	cardType = 1;
	cardHolderName = 'John Smith';

	constructor(private httpClient: HttpClient, private messageBus: EventAggregator) {
		this.messageBus.subscribe(Events.BookRoom, () => {
			this.submitPayment();
		});
	}

	submitPayment() {
		let url = 'http://localhost:54626/api/payment';
		let body = {
			"paymentMethodUuid": uniqid(),
			"purchaseUuid": shoppingCart.reservationUuid,
			"card": {
				"cardHolderName": this.cardHolderName,
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