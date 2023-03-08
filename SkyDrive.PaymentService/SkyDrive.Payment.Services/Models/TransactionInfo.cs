namespace SkyDrive.Payment.Services.Models
{
    public class TransactionInfo
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CreditCardNumber { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public string ExpirationMonth { get; set; } = null!;
        public string ExpirationYear { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
