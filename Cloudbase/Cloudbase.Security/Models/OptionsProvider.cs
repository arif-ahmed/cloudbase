
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Cloudbase.Security.Models
{
    public class OptionsProvider : IOptions<IdentityOptions>
    {
        public IdentityOptions Value
        {
            get
            {
                var result = new IdentityOptions();
                result.ClaimsIdentity = new ClaimsIdentityOptions { UserIdClaimType = "sub" };
                return result;
            }
        }
    }
}
