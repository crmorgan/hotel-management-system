
export class BookingState {
  guestSubmitted: boolean;
  reservationSubmitted: boolean;
  paymentSubmitted: boolean;

  isBookingSubmitted() {
    return this.guestSubmitted && this.reservationSubmitted && this.paymentSubmitted;
  }
}