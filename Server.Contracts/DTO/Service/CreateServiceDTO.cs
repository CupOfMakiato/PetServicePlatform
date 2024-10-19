using Microsoft.AspNetCore.Http;
using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Service
{
    public class CreateServiceDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ThumbNail { get; set; }
        public Guid SubCategoryId { get; set; }
        public ServiceType Type { get; set; }
        public double Price { get; set; }
        public string? ThumbNailUrl { get; set; }
        public string? ThumbNailId { get; set; }
        public bool isVerified { get; set; }
    }
}
