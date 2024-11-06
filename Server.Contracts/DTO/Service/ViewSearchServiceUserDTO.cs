using Server.Contracts.DTO.User;
using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Service
{
    public class ViewSearchServiceUserDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid SubCategoryId { get; set; }
        public string ThumbNail { get; set; }
        public ServiceType Type { get; set; }
        public double? Price { get; set; }
        public SearchServiceUserDTO? CreatedByUser { get; set; }
    }
}
