using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Payment
{
    public class PaymentDto
    {
        public string? Id { get; set; } = string.Empty;
        public string? PaymentContent { get; set; } = string.Empty;
        public string? PaymentCurrency { get; set; } = string.Empty;
        public string? PaymentRefId { get; set; } = string.Empty;
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now;
        public string? PaymentLanguage { get; set; } = string.Empty;
        public string? MerchanId { get; set; } = string.Empty;
        public string? PaymetDestinationId { get; set; } = string.Empty;
        public decimal? PaymentAmount { get; set; }
        public string? PaymetStatus { get; set; } = string.Empty;
        public string? PaymetLastMessage { get; set; } = string.Empty;
    }
}
