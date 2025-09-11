using Abstractions;
using System.Security.Claims;

namespace Api.Http
{
    public class CurrentHttpContextAccessor : ICurrentHttpContextAccessor
    {
        public string IdentityClientId { get; set; } = null!;
        public string? IdentityUserId { get; set; }
        public string[] UserRoles { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }

        public void SetContext(HttpContext context)
        {
            if (!string.IsNullOrEmpty(IdentityClientId) || !string.IsNullOrEmpty(IdentityUserId) || !string.IsNullOrEmpty(MethodName))
            {
                return;
            }
            var user = context.User;
            var claims = user.Claims.ToList();
            IdentityClientId = user.Claims.FirstOrDefault(x => x.Type == "azp")?.Value ??
                               throw new ApplicationException("client_id не задан в токене!");

            IdentityUserId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            UserRoles = user.FindAll("role").Select(x => x.Value).ToArray();

            MethodName = context.Request.Method;

            UserName = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

            UserSurname = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
        }
    }
}
