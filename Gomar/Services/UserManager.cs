using Gomar.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Authentication;
using System.Security.Claims;

namespace Gomar.Services
{
    public class UserManager
    {
        private readonly IAdminUser _adminUser;
        public UserManager(IAdminUser adminUser)
        {
            _adminUser = adminUser;
        }

        public async Task SignIn(HttpContext httpContext, string email, string password)
        {
            if(email == _adminUser.Email && password == _adminUser.Password)
            {
                ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            else
            {
                throw new AuthenticationException("Niepoprawny email lub hasło");
            }
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> GetUserClaims()
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, _adminUser.Name));
            claims.Add(new Claim(ClaimTypes.Email, _adminUser.Email));
            return claims;
        }
    }
}
