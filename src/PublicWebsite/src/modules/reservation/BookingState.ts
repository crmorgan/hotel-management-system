
export class BookingState {
  guestSubmitted: boolean = false;
  paymentSubmitted: boolean = false;
  rateSubmitted: boolean = false;

  hasAllDataBeenSubmitted() {
    return this.guestSubmitted && this.paymentSubmitted && this.rateSubmitted;
  }
}
