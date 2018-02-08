
export class BookingState {
  guestSubmitted: boolean = false;
  // reservationSubmitted: boolean = false;
  paymentSubmitted: boolean = false;
  rateSubmitted: boolean = false;

  isBookingSubmitted() {
    return this.guestSubmitted && this.paymentSubmitted && this.rateSubmitted;
  }
}
