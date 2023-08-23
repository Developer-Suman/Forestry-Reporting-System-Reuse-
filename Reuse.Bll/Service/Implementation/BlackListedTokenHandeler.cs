using Microsoft.AspNetCore.Authorization;
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
    public class BlackListedTokenHandeler: AuthorizationHandler<BlackListedTokenRequirement>
    {
        private readonly IBlackListServices _tokenBlackList;

        public BlackListedTokenHandeler(IBlackListServices tokenBlacklist)
        {
            _tokenBlackList = tokenBlacklist;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BlackListedTokenRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext != null)
            {
                var jwt = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(jwt))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var jwtHandeler = new JwtSecurityTokenHandler();
                var token = jwtHandeler.ReadToken(jwt) as JwtSecurityToken;
                if (_tokenBlackList.IsBlacklisted(token.Id))
                {
                    context.Fail();
                    return Task.CompletedTask;

                }

            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
