using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Service
{
    public class ServiceListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubCategoryId { get; set; }
        public Double Price { get; set; }
        public string ThumbNail { get; set; }
        public string Type { get; set; }
    }
}
