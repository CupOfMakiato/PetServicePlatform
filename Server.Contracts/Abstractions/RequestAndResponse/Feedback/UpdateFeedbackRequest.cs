using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.RequestAndResponse.Feedback
{
    public class UpdateFeedbackRequest
    {
        public string? FeedbackContent { get; set; }
        public int? Rating { get; set; }
    }
}
