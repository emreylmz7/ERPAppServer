using ERPServer.Application.Features.Auth.Register;
using ERPServer.Application.Services;
using ERPServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace ERPServer.Application.Features.Auth.Register
{
    internal sealed class RegisterCommandHandler(
        UserManager<AppUser> userManager,
        IJwtProvider jwtProvider) : IRequestHandler<RegisterCommand, Result<RegisterCommandResponse>>
    {
        public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            IdentityResult result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (500, $"Kullanıcı oluşturulamadı: {errors}");
            }

            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);

            var tokenResult = await jwtProvider.CreateToken(user);

            var registerCommandResponse = new RegisterCommandResponse(
                tokenResult.Token,
                tokenResult.RefreshToken,
                tokenResult.RefreshTokenExpires
            );

            return registerCommandResponse;
        }
    }
}
