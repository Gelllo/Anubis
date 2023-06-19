using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;

namespace Anubis.Application.Requests.Login
{
    public class LoginRequestValidator : Validator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName can not be empty");
            RuleFor(x=> x.Password).NotEmpty().WithMessage("Password ca not be empty");

        }
    }
}
