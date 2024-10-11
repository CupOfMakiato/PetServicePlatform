using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IOtpService
    {
        Task StoreOtpAsync(int userId, string otp, TimeSpan expiration);
        Task<bool> ValidateOtpAsync(int userId, string otp);
    }
}
