using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Encodings.Web;

namespace ApiBeginner.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());

            var authHeader = Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Unkown Schema"));

            var encodedCredentials = authHeader.Substring("Basic ".Length);
            var decodedCredentials = Encoding.UTF8.GetString( Convert.FromBase64String(encodedCredentials));
            var userNameAndPassword = decodedCredentials.Split(':');
            if (userNameAndPassword[0]!="admin" || userNameAndPassword[1]!="pass")
                return Task.FromResult(AuthenticateResult.Fail("User name or password invalid!"));

            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
}
