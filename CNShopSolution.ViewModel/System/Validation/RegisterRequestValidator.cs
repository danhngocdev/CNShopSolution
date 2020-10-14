using CNShopSolution.ViewModel.System.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.System.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("First name can not over 200 characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required")
                .MaximumLength(200).WithMessage("Last name can not over 200 characters");

            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddDays(-100)).WithMessage("Birthday cannot greater than 100 years");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email fomat not match");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");

            RuleFor(x => x.PassWord).NotEmpty().WithMessage("PassWord is required")
                .MinimumLength(6).WithMessage("PassWord is at least 6 characters");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.PassWord != request.ConfirmPassWord)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });


        }
    }
}
