namespace Ecommerce.API.Models.DTO
{
    public class UpdateUPaymentRequestDto
    {
        public int UserId { get; set; } 
        public string PaymentType { get; set; } = null!;

        public string Provider { get; set; } = null!;

        public int AccountNo { get; set; }

        public string Expiry { get; set; }
    }
}
