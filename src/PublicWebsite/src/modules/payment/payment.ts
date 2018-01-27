import {autoinject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
var uniqid = require('uniqid');
import shoppingCart from "../../shoppingCart";

const Events = {
  BookRoom: 'BookRoom',
  PaymentSubmitted: 'PaymentSubmitted'
}


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
		let url = 'http://localhost:59119/api/paymentMethods';
		let body = this.createPaymentPayload();

		this.sendPaymentRequest(url, body)
	}

	private createPaymentPayload() {
		return  {	
					"paymentMethodUuid": uniqid(),
					"purchaseUuid": shoppingCart.reservationUuid,
					"card": {
						"cardHolderName": this.cardHolderName,
						"number": this.cardNumber,
						"typeId": this.cardType,
						"expiration": this.cardExpiration
					}
				}
	}

	private sendPaymentRequest(url, body) {
		this.httpClient.fetch(url, {
			method: 'PUT',
			body: json(body)
		})
		.then(response => response.json())
		.then(data => {
      		this.messageBus.publish(Events.PaymentSubmitted);
		});
	}
}
