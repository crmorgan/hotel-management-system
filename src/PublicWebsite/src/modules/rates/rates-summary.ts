import { EventAggregator } from 'aurelia-event-aggregator'
import { autoinject, bindable } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import shoppingCart from "../../shoppingCart";

const Events = {
  RatesFetched: 'RatesFetched',
  RatesSummaryFetched: 'RatesSummaryFetched',
  RateSubmitted: 'RateSubmitted'
}

@autoinject()
export class RatesSummary {
  numberOfNights: number;
  nightlyRate: number;
  totalAmount: number;

  constructor(private messageBus: EventAggregator, private httpClient: HttpClient) {
    this.numberOfNights = shoppingCart.numberOfNights;
    this.getRate();
  }

  activate() {
  }

  getRate() {
    let url = 'http://localhost:54520/api/roomTypeRates?' + 'ids=' + shoppingCart.roomTypeId + '&checkin=' + shoppingCart.checkin.toLocaleDateString() + '&checkout=' + shoppingCart.checkout.toLocaleDateString();

    this.httpClient.fetch(url)
      .then(response => response.json())
      .then(data => {
        let rate = data[0];

        // this.messageBus.publish(Events.RatesSummaryFetched, rate);

        this.nightlyRate = rate.amount;
        this.totalAmount = rate.amount * shoppingCart.numberOfNights;
        this.numberOfNights = shoppingCart.numberOfNights;

        this.submitReservation(rate.rateId, this.totalAmount);
      });
  }

  submitReservation(rateId: number, totalAmount: number) {
    let url = `http://localhost:54520/api/reservations/${shoppingCart.reservationUuid}`;
    let body = this.createPutReservationPayload(rateId, totalAmount);

    this.sendPutReservation(url, body);
  }

  private createPutReservationPayload(rateId: number, totalAmount: number) {
    return {
      "roomRateId": rateId,
      "totalAmount": totalAmount,
    }
  }

  private sendPutReservation(url, body) {
    this.httpClient.fetch(url, {
      method: 'PUT',
      body: json(body)
    })
      .then(response => response.json())
      .then(data => {
        this.messageBus.publish(Events.RateSubmitted);
      });
  }
}
