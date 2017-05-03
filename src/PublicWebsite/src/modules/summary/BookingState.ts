
export class BookingState {
  guestSubmitted: boolean = false;
  reservationSubmitted: boolean = false;
  paymentSubmitted: boolean = false;

  isBookingSubmitted() {
    return this.guestSubmitted && this.reservationSubmitted && this.paymentSubmitted;
  }
}