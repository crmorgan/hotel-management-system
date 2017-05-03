import {autoinject} from 'aurelia-framework';
import {EventAggregator} from 'aurelia-event-aggregator';
import {Events} from '../../messages/events';
import { BookingState } from './BookingState';
import { Router } from 'aurelia-router';

@autoinject()
export class Summary {
  private bookingState: BookingState;

  constructor(private messageBus: EventAggregator, private router: Router) {
    this.bookingState = new BookingState();

    this.messageBus.subscribeOnce(Events.GuestSubmitted, () => {
      this.bookingState.guestSubmitted = true;
      this.isBookingComplete();
    });

    this.messageBus.subscribeOnce(Events.PaymentSubmitted, () => {
      this.bookingState.paymentSubmitted = true;
      this.isBookingComplete();
    });

    this.messageBus.subscribeOnce(Events.ReservationSubmitted, () => {
      this.bookingState.reservationSubmitted = true;
      this.isBookingComplete();
    });
  }

  isBookingComplete() {
    if (this.bookingState.isBookingSubmitted()) {
      console.log("navigate");
      this.router.navigate('confirmation');
    }
  }

	book() {
		this.messageBus.publish(Events.BookRoom);		
	}
}