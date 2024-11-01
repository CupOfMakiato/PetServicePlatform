using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Server.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbNail { get; set; }
        public string ThumbNailId { get; set; }
        public double Price { get; set; }
        public ICollection<Booking> Booking { get; set; }
        public bool isVerified { get; set; }
        [Required]
        public ServiceType? Type { get; set; }

        public string? Reason { get; set; }
        public Guid CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public List<Transaction> Transaction { get; set; }
        public List<BillDetail> BillDetail { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
