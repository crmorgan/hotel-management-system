namespace Payments.Messages.Events
{
	public class PaymentSucceededEvent
	{
		public string ReservationUuid { get; set; }
		public int RoomTypeId { get; set; }
		public virtual bool IsReservationSubmitted { get; set; }
		public virtual bool IsPaymentProcessed { get; set; }
	}
}