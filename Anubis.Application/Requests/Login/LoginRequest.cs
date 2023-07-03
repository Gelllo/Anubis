using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Application.Requests.Login
{
    public record LoginRequest
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
