using Microsoft.AspNetCore.Http;
using Server.Contracts.Abstractions.RequestAndResponse.Payment;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IPayoutService
    {
        Task<string> CreatePayment(Guid bookingId);
        Task<VnPaymentResponseModel> PaymentExcute(IQueryCollection collection);
    }
}
