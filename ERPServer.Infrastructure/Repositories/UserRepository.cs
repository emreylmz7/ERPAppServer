using ERPServer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ERPServer.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Guid?> GetCurrentUserId()
        {
            // Kullanıcı claims'lerini al
            var userClaims = _httpContextAccessor.HttpContext?.User?.Claims;

            if (userClaims == null || !userClaims.Any())
            {
                return Task.FromResult<Guid?>(null); // Eğer claims yoksa, null döndür
            }

            // "Id" claim'ini al
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "Id"); // "Id" claim'ini kullanıyoruz

            if (userIdClaim == null)
            {
                return Task.FromResult<Guid?>(null); // Eğer "Id" claim'i yoksa, null döndür
            }

            var userGuid = Guid.Parse(userIdClaim.Value); // "Id" claim'inden Guid değerini al
            return Task.FromResult<Guid?>(userGuid); // Kullanıcı ID'sini döndür
        }
    }
}
