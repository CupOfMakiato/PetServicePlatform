﻿using Server.Contracts.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IEmailService
    {
        // General email functions
        Task SendEmailAsync(EmailDTO request);

        // Activation/Deactivation email functions
        Task SendActiveEmailAsync(string email);
        Task SendDeactiveEmailAsync(string email, string reason);

        // Verification and OTP email functions
        Task SendVerificationEmailAsync(string email, string token);
        Task SendOtpEmailAsync(string email, string otp);
    }
}