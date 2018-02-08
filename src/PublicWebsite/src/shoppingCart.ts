class ShoppingCart {
	checkin: Date = new Date()
	checkout: Date = new Date()
	reservationUuid: ''
	roomTypeIds: ''
	roomTypeId: ''
  get numberOfNights(): number {
    return new Date(this.checkout.getTime() - this.checkin.getTime()).getUTCDate() -1;
  }
}

var shoppingCart = new ShoppingCart();
export default shoppingCart;
