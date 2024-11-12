namespace ERPServer.Application.Features.Auth.Register
{
    public sealed record RegisterCommandResponse(
        string Token,
        string RefreshToken,
        DateTime RefreshTokenExpires);
}
