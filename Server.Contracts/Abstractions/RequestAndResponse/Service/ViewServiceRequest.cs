using Microsoft.AspNetCore.Http;
using Server.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.RequestAndResponse.Service
{
    public class ViewServiceRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ServiceType Type { get; set; }
        public IFormFile ThumbNail { get; set; }
        public string? ThumbNailUrl { get; set; }
        public string? ThumbNailId { get; set; }
    }
}
