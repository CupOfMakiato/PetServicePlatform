using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Server.Contracts.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Payment
{
    public class GetPayment : PaymentDto
    {
        public string Id { get; set; } = string.Empty;
    }
}
