﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public ApplicationUser User { get; set; }
        public Service Service { get; set; }
    }
}
