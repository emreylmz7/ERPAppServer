using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Auth.Register
{
    public sealed record RegisterCommand(
        string UserName,
        string Email,
        string Password,
        string FirstName,
        string LastName) : IRequest<Result<RegisterCommandResponse>>;

}
