using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;
using FastEndpoints;
using FluentValidation;

namespace Anubis.Application.Requests.Register
{
    public class RegisterRequestValidator : Validator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x=> x.UserID).NotEmpty().WithMessage("UserName can not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password can not be empty");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("The passwords don't match!");
            RuleFor(x => x.Role).Must(y => UserRoles.roles.Contains(y)).WithMessage("Invalid user Role");
        }
    }
}
