import { autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { EventAggregator } from 'aurelia-event-aggregator';
import shoppingCart from "../../shoppingCart";
import { GuestsEvent } from "../guests/guests-event"
var uniqid = require('uniqid');

const Events = {
  BookRoom: 'BookRoom'
}


@autoinject()
export class Guests {
  reservationUuid = shoppingCart.reservationUuid;
  title = 'Mr.';
  firstName = 'John';
  lastName = 'Smith';
  email = 'john.smith@email.com';
  line1 = '1234 Main st';
  city = "Denver";
  state = 'CO';
  zip = '80202';

  constructor(private httpClient: HttpClient, private messageBus: EventAggregator) {
    this.messageBus.subscribeOnce(Events.BookRoom, () => {
      this.submitGuest();
    });
  }

  submitGuest() {
    console.log(`submitting guest for reservation ${shoppingCart.reservationUuid}`);
    let url = 'http://localhost:50551/api/guests';
    let body = this.createGuestPayload();

    this.sendGuestRequest(url, body);
  }

  private createGuestPayload() {
    return {
      "guestUuid": uniqid(),
      "reservationUuid": shoppingCart.reservationUuid,
      "title": this.title,
      "firstName": this.firstName,
      "lastName": this.lastName,
      "email": this.email,
      "address": {
        "line1": this.line1,
        "city": this.city,
        "state": this.state,
        "zip": this.zip
      }
    }
  }

  private sendGuestRequest(url, body) {
    this.httpClient.fetch(url, {
      method: 'PUT',
      body: json(body)
    })
      .then(response => response.json())
      .then(data => {
        this.messageBus.publish(GuestsEvent.GuestSubmitted);
      });
  }
}
