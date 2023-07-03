using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.ForgotPassword
{
    public record ResetPasswordRequest
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
