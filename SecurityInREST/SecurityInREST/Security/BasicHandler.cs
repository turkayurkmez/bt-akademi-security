using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SecurityInREST.Security
{
    public class BasicHandler : AuthenticationHandler<BasicOption>
    {
        public BasicHandler(IOptionsMonitor<BasicOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            /*
             *  $.ajax({
                    headers:{
                        'Authorization':'Basic '+btoa('testuser:123') 
                    },
                    url:'https://localhost:7155/WeatherForecast',
                    });
             */
            /*
             * 1. Gelen http request'de 'Authorization' header'ı var mı?
             * 2. Bu header formata uygun mu?
             * 3. Varsa bu Authorization header'i Basic mi?
             * 4. Şema değeri uygun mu?
             * 5. Decode et. ':' işaretine göre ayır. İlki kullanıcı adı ikincisi şifredir. 
             */

            //1. Gelen http request'de 'Authorization' header'ı var mı?

            const string headerKeyForAuthorize = "Authorization";

            if (!Request.Headers.ContainsKey(headerKeyForAuthorize))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //2. Bu header formata uygun mu?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers[headerKeyForAuthorize], out AuthenticationHeaderValue parsedValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }
            //3. Authorization header'i Basic mi?
            if (!parsedValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            /*
             *  4. Şema değeri uygun mu?
             * 5. Decode et. ':' işaretine göre ayır. İlki kullanıcı adı ikincisi şifredir. 
             */

            var bytes = Convert.FromBase64String(parsedValue.Parameter);
            var userNameAndPass = Encoding.UTF8.GetString(bytes);
            var userName = userNameAndPass.Split(':')[0];
            var password = userNameAndPass.Split(':')[1];

            if (userName == "testuser" && password =="123")
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,"test")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Hatalı giriş"));

        }
    }
}
