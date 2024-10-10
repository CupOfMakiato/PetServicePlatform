using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string FeedbackDetail { get; set; }
        public DateTime DateCreateFeedback { get; set; }
        public int Rating { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
