using FluentValidation;
using Microsoft.AspNetCore.Http;
using Server.Contracts.Abstractions.RequestAndResponse.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Validations.ServiceValidate
{
    public class CreateApproveOrRejectValidator : AbstractValidator<CreateApproveOrRejectRequest>
    {
        public CreateApproveOrRejectValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Id can not be blanked!.");
        }
    }
}
