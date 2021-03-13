using System;
using System.ComponentModel.DataAnnotations;

namespace Filed_Coding.Data.Models
{
    public class Payment : BaseEntity<Guid>
    {
        [MaxLength(16)]
        [MinLength(16)]
        [Required]
        public string CreditCardNumber { get; set; }
        [MaxLength(150)]
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [MaxLength(3)]
        public string SecurityCode { get; set; }
        [Required]
        public Decimal Amount { get; set; }
    }
}
