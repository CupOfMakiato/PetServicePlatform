﻿using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Auth;
using Server.Contracts.DTO.Shop;
using Server.Contracts.DTO.User;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IAuthService
    {
        //LOGIN
        Task<Authenticator> LoginAsync(LoginDTO loginDTO);
        Task<Authenticator> RefreshToken(string token);
        Task<bool> DeleteRefreshToken(Guid userId);

        //REGISTER
        Task RegisterUserAsync(UserRegistrationDTO userRegistrationDto);
        Task<ApplicationUser> GetByVerificationToken(string token);
        Task<bool> VerifyOtpAsync(string email, string otp);

        //REGISTER INSTRUCTOR
        Task RegisterInstructorAsync(ShopRegisterDTO shopRegisterDTO);
        Task<bool> VerifyOtpAndCompleteRegistrationAsync(string email, string otp);
        //Google
        /*        Task<Result<object>> UserCompleteSignUpByGoogle(SignupGoogleRequest userRegistrationDto);*/
        //CHANGE PASSWORD 
        /*        Task ChangePasswordAsync(string email, ChangePasswordDTO changePasswordDto);*/

        //FORGOT PASSWORD
        /*        Task RequestPasswordResetAsync(ForgotPasswordRequestDTO forgotPasswordRequestDto);
                Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDto);*/
    }
}
