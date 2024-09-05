using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.Abstractions.Topic
{
    public class ViewTopicRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
