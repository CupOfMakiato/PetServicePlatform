using AutoMapper;
using Microsoft.AspNetCore.Http;
using Server.Application.Common;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Booking;
using Server.Contracts.DTO.Service;
using Server.Contracts.Enum;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserService _userService;
        public BookingService(IBookingRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IServiceRepository serviceRepository, IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _serviceRepository = serviceRepository;
            _userService = userService;
        }

        public async Task<Result<IEnumerable<GetBookingDto>>> GetListBookingByShopId(int PAGE_SIZE = 10, int page = 1)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
                return new Result<IEnumerable<GetBookingDto>>() { Error =  1, Message = "Token not found", Data = null };

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return new Result<IEnumerable<GetBookingDto>>() { Error = 1, Message = "Invalid token", Data = null };
            var userId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == "id").Value);
            var bookings = await _repository.GetListBookingByShopId(userId);
            var listBookings = new List<GetBookingDto>();
            var result = PaginatedList<Booking>.Create(bookings, page, PAGE_SIZE);
            foreach (var booking in result)
            {
                var bookingDto = new GetBookingDto()
                {

                };
                listBookings.Add(bookingDto);
            }
            return new Result<IEnumerable<GetBookingDto>>() { Error = 0, Message = "", Data = listBookings };
        }

        public async Task<Result<IEnumerable<GetBookingDto>>> GetListBookingByUserId(int PAGE_SIZE = 10, int page = 1)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
                return new Result<IEnumerable<GetBookingDto>>() { Error = 1, Message = "Token not found", Data = null };

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return new Result<IEnumerable<GetBookingDto>>() { Error = 1, Message = "Invalid token", Data = null };
            var userId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == "id").Value);
            var bookings = await _repository.GetListBookingByUserId(userId);
            var listBookings = new List<GetBookingDto>();
            var result = PaginatedList<Booking>.Create(bookings, page, PAGE_SIZE);
            foreach (var booking in result)
            {
                var bookingDto = new GetBookingDto()
                {

                };
                listBookings.Add(bookingDto);
            }
            return new Result<IEnumerable<GetBookingDto>>() { Error = 0, Message = "", Data = listBookings };
        }

        public async Task<Result<object>> AddBooking(AddBookingDto addBookingDto)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
                return new Result<object>() { Error = 1, Message = "Token not found", Data = null };

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return new Result<object>() { Error = 1, Message = "Invalid token", Data = null };
            var userId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == "id").Value);
            var getService = await _serviceRepository.GetServiceById(addBookingDto.ServiceId);
            if(getService == null)
            {
                return new Result<object>() { Error = 1, Message = "Service not found", Data = null };
            }
            if(addBookingDto.OptionPay == OptionPay.CashPayment)
            {
                var bookingCash = new Booking()
                {
                    UserId = userId,
                    ServiceId = addBookingDto.ServiceId,
                    FullName = addBookingDto.FullName,
                    PhoneNumber = addBookingDto.PhoneNumber,
                    BookingDate = addBookingDto.BookingDate,
                    OptionPay = OptionPay.CashPayment.ToString(),
                    IsPayment = false
                };
                await _repository.AddBooking(bookingCash);
                return new Result<object>() { Error = 0, Message = "Booking Successfully", Data = bookingCash };
            }
            var bookingOnl = new Booking()
            {
                UserId = userId,
                ServiceId = addBookingDto.ServiceId,
                FullName = addBookingDto.FullName,
                PhoneNumber = addBookingDto.PhoneNumber,
                BookingDate = addBookingDto.BookingDate,
                OptionPay = OptionPay.CashPayment.ToString(),
                IsPayment = true
            };
            await _repository.AddBooking(bookingOnl);
            return new Result<object>() { Error = 0, Message = "Booking Successfully", Data = bookingOnl };
        }
    }
}
