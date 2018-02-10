import { autoinject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { BookingState } from '../../modules/reservation/BookingState';
import { Router } from 'aurelia-router';
import { RatesEvent } from "../../modules/rates/rates-event";
import { GuestsEvent } from "../../modules/guests/guests-event";
import { FinanceEvent } from "../../modules/finance/finance-event";

export const Events = {
  BookRoom: 'BookRoom'
}

@autoinject()
export class Summary {
  private bookingState: BookingState;

  constructor(private messageBus: EventAggregator, private router: Router) {
    this.bookingState = new BookingState();

    this.messageBus.subscribeOnce(GuestsEvent.GuestSubmitted, () => {
      this.bookingState.guestSubmitted = true;
      this.isBookingComplete();
    });

    this.messageBus.subscribeOnce(FinanceEvent.PaymentSubmitted, () => {
      this.bookingState.paymentSubmitted = true;
      this.isBookingComplete();
    });

    // this.messageBus.subscribeOnce(Events.ReservationSubmitted, () => {
    //   this.bookingState.reservationSubmitted = true;
    //   this.isBookingComplete();
    // });

    this.messageBus.subscribeOnce(RatesEvent.RateSubmitted, () => {
      this.bookingState.rateSubmitted = true;
      this.isBookingComplete();
    });
  }

  isBookingComplete() {
    if (this.bookingState.hasAllDataBeenSubmitted()) {
      this.router.navigate('checkout/confirmation');
    }
  }

  book() {
    console.log(`publishing ${Events.BookRoom} event`);
    this.messageBus.publish(Events.BookRoom);
  }
}
