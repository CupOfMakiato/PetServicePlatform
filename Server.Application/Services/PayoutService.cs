using Google.Apis.Requests.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Utils;
using Server.Contracts.Abstractions.RequestAndResponse.Payment;
using Server.Contracts.Abstractions.Shared;
using Server.Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class PayoutService : IPayoutService
    {
        private readonly IConfiguration _configuration;
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PayoutService(IConfiguration configuration, IBookingRepository bookingRepository, IServiceRepository serviceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreatePayment(Guid bookingId)
        {
            var getBooking = await _bookingRepository.GetBookingById(bookingId);
            if (getBooking == null) throw new Exception("Booking not found.");

            var getService = await _serviceRepository.GetByIdAsync(getBooking.ServiceId);
            if (getService == null) throw new Exception("Service not found.");

            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _configuration["VnpaySettings:Version"]);
            vnpay.AddRequestData("vnp_Command", _configuration["VnpaySettings:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _configuration["VnpaySettings:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (getService.Price * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", getBooking.BookingDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _configuration["VnpaySettings:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.Utils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnpay.AddRequestData("vnp_Locale", _configuration["VnpaySettings:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + bookingId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", _configuration["VnpaySettings:ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl = vnpay.CreateRequestUrl(_configuration["VnpaySettings:Url"], _configuration["VnpaySettings:HashSecret"]);

/*            var payment = new Payment()
            {
                UserId = getBooking.UserId,
                TotalAmount = getService.Price,
                CreationDate = DateTime.UtcNow.AddHours(7),
                PaymentUrl = paymentUrl,
                PaymentStatus = "Pending",
                PaymentMethod = "VNPAY"
            };*/

            return paymentUrl;
        }

        public VnPaymentResponseModel PaymentExcute(IQueryCollection collection)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collection)
            {
                if(!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_bookingId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_bookingInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _configuration["VnpaySettings:HashSecret"]);
            if(!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false,
                };
            }
            return new VnPaymentResponseModel()
            {
                Success = true,
                PaymentMethod = "VnPay",
                BookingDescription = vnp_bookingInfo,
                BookingId = vnp_bookingId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }
    }
}
