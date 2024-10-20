using Server.Contracts.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Service
{
    public class ServiceDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubCategoryId { get; set; }
        public Double Price { get; set; }
        public string ThumbNail { get; set; }
        public string Type { get; set; }

        //public ICollection<User> Users { get; set; }

        public Guid CreatedByUserId { get; set; }
        public UserDTO CreatedByUser { get; set; }
    }
}
