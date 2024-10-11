using Server.Contracts.Abstractions.RequestAndResponse.Feedback;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Validations.Feedback
{
    public class CreateFeedbackRequestValidator :AbstractValidator<CreateFeedbackRequest>
    {
        public CreateFeedbackRequestValidator()
        {
            RuleFor(x => x.FeedbackContent).NotEmpty().WithMessage("You can not leave the field empty here!");

            RuleFor(x => x.Rating).NotEmpty().WithMessage("You can not leave the field empty here!");

            RuleFor(x => x.UserId).NotEmpty().WithMessage("must not be blanked!.");

            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("must not be blanked!.");
        }
    }
}
