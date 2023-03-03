namespace SkyDrive.Payment.Services.Models
{
    public class TransactionInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public decimal Price { get; set; }
    }
}
