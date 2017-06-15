import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {EventAggregator} from 'aurelia-event-aggregator';
import shoppingCart from "../../shoppingCart";
import {Router} from 'aurelia-router';

const Events = {
  RatesSummaryFetched: 'RatesSummaryFetched',
  BookRoom: 'BookRoom',
  ReservationSubmitted: 'ReservationSubmitted'
}


@autoinject()
export class ReservationAmount {
	amount;

	constructor(private httpClient: HttpClient, private messageBus: EventAggregator, private router: Router) {
		this.messageBus.subscribe(Events.RatesSummaryFetched, message => {
      this.amount = message.Amount;
    });

	  this.messageBus.subscribe(Events.BookRoom, () => {
	    this.submitReservation();
	  });
	}

	submitReservation() {
    let url = 'http://localhost:54626//api/reservations/' + shoppingCart.reservationUuid + '/rates';

//	  this.messageBus.publish(Events.ReservationSubmitted);
	  let body = {
	    "rate": parseInt(this.amount)
	  }

		this.httpClient.fetch(url,{
			method: 'PUT',
      body: json(this.amount)
			
		})
		.then(response => response.json())
		.then(data => {
      this.messageBus.publish(Events.ReservationSubmitted);
		});
	}
}