using FluentValidation;
using Server.Contracts.Abstractions.RequestAndResponse.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Validations.Feedback
{
    public class UpdateFeedbackRequestValidator: AbstractValidator<UpdateFeedbackRequest>
    {
        public UpdateFeedbackRequestValidator()
        {
            RuleFor(x => x.FeedbackContent).NotEmpty().WithMessage("You can not leave the field empty here!");

            RuleFor(x => x.Rating).NotEmpty().WithMessage("You can not leave the field empty here!");
        }
    }
}
