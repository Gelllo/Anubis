using Anubis.Domain;

namespace Anubis.Application.Responses.Users
{
    public record GetUsersResponse
    {
        public IEnumerable<User>? UsersList { get; set; }
    }
}
