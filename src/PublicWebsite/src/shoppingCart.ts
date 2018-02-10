class ShoppingCart {
	checkin: Date = new Date()
	checkout: Date = new Date()
	reservationUuid: ''
  roomTypeId: ''
  
  get numberOfNights(): number {
    return new Date(this.checkout.getTime() - this.checkin.getTime()).getUTCDate() -1;
  }

  public clear(){
    this.checkin = new Date();
    this.checkout = new Date();
    this.reservationUuid = '';
    this.roomTypeId = '';
    console.log(`cart cleared`);
  }
}

var shoppingCart = new ShoppingCart();
export default shoppingCart;
