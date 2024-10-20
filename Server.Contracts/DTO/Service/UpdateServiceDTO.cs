using Microsoft.AspNetCore.Http;
using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Service
{
    public class UpdateServiceDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? ThumbNail { get; set; }
        //[Required]
        //[RegularExpression("Dog|Cat", ErrorMessage = "Type must be either 'Dog' or 'Cat'")]
        //public string Type { get; set; }
        public ServiceType Type { get; set; }
        public double Price { get; set; }
        public Guid SubCategoryId { get; set; }
    }
}
