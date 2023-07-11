using System.Linq.Expressions;
using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Application;
using FastEndpoints;

public class GetUser : EndpointWithoutRequest
{
    private readonly IQueryDispatcher _dispatcher;
    private ILogger _logger;

    public GetUser(IQueryDispatcher dispatcher, ILogger logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }
    public override void Configure()
    {
        Get("/users/{UserID}");
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        try
        {
            var req = new GetUserRequest() { UserID = Route<string>("UserID") };
            await SendAsync(await _dispatcher.Dispatch<GetUserRequest, GetUserResponse>(req, c));
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
        }
    }
}