﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.CloudinaryService
{
    public class CloudinaryResponse
    {
        public string? ImageUrl { get; set; }
        public string? PublicImageId { get; set; }
        public string? PublicVideoId { get; set; }
    }
}
