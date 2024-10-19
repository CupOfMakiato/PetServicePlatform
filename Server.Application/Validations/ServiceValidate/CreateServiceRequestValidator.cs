﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Server.Contracts.Abstractions.RequestAndResponse.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Validations.ServiceValidate
{
    public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
    {
        public CreateServiceRequestValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.SubCategoryId)
                .NotEmpty().WithMessage("SubCategoryId is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.");

            RuleFor(x => x.ThumbNail)
                .Must(BeAValidImage).WithMessage("File must be a valid image (jpg, jpeg, png) and less than or equal to 2MB.");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null)
                return true;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                return false;

            if (file.Length > 2 * 1024 * 1024)
                return false;

            return true;
        }
    }
}
