using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Feedback
{
    public class CreateFeedbackDTO
    {
        Guid Id { get; set; }
        public string? FeedbackContent { get; set; }
        public int? Rating { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
