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

    this.messageBus.subscribe(Events.GuestSubmitted, () => {
      this.bookingState.guestSubmitted = true;
      this.isBookingComplete();
    });

    this.messageBus.subscribe(Events.PaymentSubmitted, () => {
      this.bookingState.paymentSubmitted = true;
      this.isBookingComplete();
    });

    this.messageBus.subscribe(Events.ReservationSubmitted, () => {
      this.bookingState.reservationSubmitted = true;
      this.isBookingComplete();
    });
  }

  isBookingComplete() {
    if (this.bookingState.isBookingSubmitted()) {
      this.router.navigate('confirmation');
    }
  }

	book() {
		this.messageBus.publish(Events.BookRoom);		
	}
}