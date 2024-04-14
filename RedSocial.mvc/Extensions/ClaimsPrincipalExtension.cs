using System.Security.Claims;

namespace RedSocial.mvc.Extension
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetIdActualIdentityUser(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static string GetUserNameActualIdentityUser(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
        }
        public static string GetEmailActualIdentityUser(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
        }
    }
}
