using Filed_Coding.Helpers.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Filed_Coding.ShearedModel.Models
{
    public class PaymentDto : DtoBase<Guid>
    { 
        public PaymentDto()
        {
            Id = Guid.Empty;
        }
        [MaxLength(16)]
        [MinLength(16)]
        [CreditCard]
        [Required]
        public string CreditCardNumber { get; set; }
        [MaxLength(150)]
        [Required]
        public string CardHolder { get; set; }
        [Required]
        [CardExpiryValidator(ErrorMessage = "Expiry Date must be a future Date.")]
        public DateTime ExpirationDate { get; set; }
        [MaxLength(3)]
        public string SecurityCode { get; set; }
        [Required]
        public Decimal Amount { get; set; }
    }
}
