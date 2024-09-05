using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.Topic
{
    public class UpdateTopicRequest
    {
        public string Name { get; set; }
        public bool status { get; set; }
    }
}
