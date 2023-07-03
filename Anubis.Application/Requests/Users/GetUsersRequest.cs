namespace Anubis.Application.Requests.Users
{
    public record GetUsersRequest
    {
        public string? order { get; set; }
        public string? sort { get; set; }
        public int? page { get; set; }
    }
}

