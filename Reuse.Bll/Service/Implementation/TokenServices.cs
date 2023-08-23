
using Microsoft.AspNetCore.Http;
using Reuse.Bll.Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Implementation
{
    public class TokenServices : ITokenServices
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUsername() {

            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null)
            {
                string authorization = httpContext.Request.Headers.Authorization;
                var token = authorization.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var uname = jwt.Claims.FirstOrDefault(x => x.Type == "username");
                return uname.Value;

            }
            return null;
        
        }
        public int GetBranchId() 
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                string authorization = httpContext.Request.Headers.Authorization;
                var token = authorization.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var bid = jwt.Claims.FirstOrDefault(x => x.Type == "bid");
                return int.Parse(bid.Value);

            }
            return 0;
        }


        public string GetRole()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null )
            {
                string authorization = httpContext.Request.Headers.Authorization;
                var token = authorization.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var role = jwt.Claims.FirstOrDefault(x => x.Type == "role");
                return role.Value;
            }
            return null;
        }


    }
}
