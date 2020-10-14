using CNShopSolution.ViewModel.System.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.System.Validation
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.PassWord).NotEmpty().WithMessage("PassWord is required")
                .MinimumLength(6).WithMessage("PassWord is at least 6 characters");

        }
    }
}
