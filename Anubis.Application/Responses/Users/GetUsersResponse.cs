using Anubis.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anubis.Application.Responses.Users
{
    public record GetUsersResponse
    {
        public IEnumerable<UserDTO>? UsersList { get; set; }
    }

    public record UserDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
