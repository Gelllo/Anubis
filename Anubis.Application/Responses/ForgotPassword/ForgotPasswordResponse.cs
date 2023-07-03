﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Responses.ForgotPassword
{
    public record ForgotPasswordResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
