using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reuse.Bll.DTO;
using Reuse.Bll.Repository.Interface;
using Reuse.Bll.Service.Interface;
using Reuse.DAL.Data;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Branch> _branch;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IBlackListServices _blackListServices;

        public AuthServices(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, IRepository<Branch> branch, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IBlackListServices blackListServices)
        {
            _dbContext = appDbContext;
            _userManager = userManager;
            _branch = branch;
            _roleManager = roleManager;
            _configuration = configuration;
            _blackListServices = blackListServices;


        }
        public async Task<ServiceResult<UserDTO>> ChangePassword(ChangePasswordDTO entity)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(entity.Username);
                if (user != null)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(user, entity.CurrentPassword, entity.NewPassword);
                    if (!changePassword.Succeeded)
                    {
                        return new ServiceResult<UserDTO>(false, errors: changePassword.Errors.Select(x => x.Description).ToArray());
                    }
                    return new ServiceResult<UserDTO>(true);
                }

                return new ServiceResult<UserDTO>(false, errors: new[] { "प्रयोगकर्ता  भेटिएन" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                List<UserDTO> userList = new();
                if (users.Count > 0)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        userList.Add(new UserDTO()
                        {
                            UserId = users[i].Id,
                            Username = users[i].UserName,
                            PhoneNumber = users[i].PhoneNumber,
                            Email = users[i].Email,
                            Branch = (await _branch.GetByIdAsync(users[i].BranchId))?.BranchName ?? null,
                            Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault(),
                            IsActive = users[i].IsActive

                        });
                    }
                    return new ServiceResult<List<UserDTO>>(true, userList);
                }
                return new ServiceResult<List<UserDTO>>(false, errors: new[] { "प्रयोगकर्ता  भेटिएन" });
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<UserDTO>>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<List<UserDTO>>> GetUserByBranchId(int BranchId)
        {
            try
            {
                var users = await _userManager.Users.Where(x => x.Branch.ParentBranchId == BranchId || x.BranchId == BranchId).ToListAsync();
                List<UserDTO> userList = new();

                if (users.Count > 0)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        userList.Add(new UserDTO()
                        {
                            UserId = users[i].Id,
                            Username = users[i].UserName,
                            PhoneNumber = users[i].PhoneNumber,
                            Email = users[i].Email,
                            Branch = (await _branch.GetByIdAsync(users[i].BranchId))?.BranchName ?? null,
                            IsActive = users[i].IsActive,
                            Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault()

                        });

                    }

                    return new ServiceResult<List<UserDTO>>(true, userList);
                }
                return new ServiceResult<List<UserDTO>>(false, errors: new[] { "प्रयोगकर्ता  भेटिएन" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<List<UserDTO>>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<TokenDTO>> LoginService(LogInDTO entity)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(entity.Username);
                if (user != null && user.IsActive && (await _branch.GetByIdAsync(user.BranchId)).IsActive && await _userManager.CheckPasswordAsync(user, entity.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var branch = await _branch.GetByIdAsync(user.BranchId);

                    var authClaims = new List<Claim>
                    {
                        new Claim("username", user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("bid", user.BranchId.ToString()),
                        //new Claim("btid", branch.BranchId.ToString()),
                        

                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim("role", userRole));

                    }

                    var token = CreateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();


                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpirtTime = DateTime.Now.AddDays(refreshTokenValidityInDays);


                    await _userManager.UpdateAsync(user);


                    TokenDTO tokenModel = new()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo,
                    };
                    return new ServiceResult<TokenDTO>(true, tokenModel);

                }

                return new ServiceResult<TokenDTO>(false, errors: new[] { "लगइन प्रयास असफल भयो ।" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<TokenDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<TokenModel>> RefreshTokenServices(TokenModel entity)
        {
            try
            {
                if (entity is null)
                {
                    return new ServiceResult<TokenModel>(false, errors: new[] { "अवैध अनुरोध। फेरि प्रयास गर्नुहोस ।" });
                }

                string? accessToken = entity.Accesstoken;
                string? refreshToken = entity.RefreshToken;

                var principal = GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    return new ServiceResult<TokenModel>(false, errors: new[] { "अवैध पहुँच टोकन वा ताजा टोकन । फेरि प्रयास गर्नुहोस" });
                }

                var username = principal.Claims.FirstOrDefault(x => x.Type == "username");

                var user = await _userManager.FindByNameAsync(username.Value);

                if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpirtTime <= DateTime.Now)
                {
                    return new ServiceResult<TokenModel>(false, errors: new[] { "अवैध पहुँच टोकन वा ताजा टोकन । फेरि प्रयास गर्नुहोस" });
                }


                //Get the claims from the original token
                var originalClaims = principal.Claims.ToList();

                //Create the new list to store the updated Claims
                var updatedClaims = new List<Claim>();

                //Iterate through the original claims and modify the necesssary ones
                foreach (var claim in originalClaims)
                {
                    if (claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    {
                        //Create the new claims with the modified type
                        var updatedClaim = new Claim("role", claim.Value);
                        updatedClaims.Add(updatedClaim);
                    }
                    else
                    {
                        updatedClaims.Add(claim);
                    }
                }

                var newAccessToken = CreateToken(updatedClaims);
                var newRefreshToken = GenerateRefreshToken();


                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                TokenModel token = new TokenModel()
                {
                    Accesstoken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken,
                };

                return new ServiceResult<TokenModel>(true, token);




            }
            catch (Exception ex)
            {
                return new ServiceResult<TokenModel>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<TokenDTO>> RegisterAdmin(RegisterDTO entity)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(entity.Username);
                var userEmailExists = await _userManager.FindByNameAsync(entity.Email);

                if (userEmailExists != null)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "माफ गर्नु होला, इमेल पहिले देखि प्रयोगमा छ " });
                }

                if (userExists != null)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "माफ गर्नु होला, प्रयोकर्ता को नाम पहिले देखि छ" });
                }

                var branchExists = await _dbContext.branches.AnyAsync(x => x.BranchId == entity.BranchId);
                if (!branchExists)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "शाखा फेला परेन" });
                }

                ApplicationUser applicationUser = new()
                {
                    Email = entity.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = entity.Username,
              
                    PhoneNumber = entity.PhoneNumber,
                    BranchId = entity.BranchId,
                    IsActive = true
                };


                var result = await _userManager.CreateAsync(applicationUser, entity.Password);
                if (!result.Succeeded)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "प्रयोगकर्ता सिर्जना असफल भयो ! कृपया प्रयोगकर्ता विवरणहरू जाँच गर्नुहोस् र पुन: प्रयास गर्नुहोस् !" });
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(applicationUser, UserRoles.Admin);
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(applicationUser, UserRoles.User);
                }

                return new ServiceResult<TokenDTO>(true);

            }
            catch (Exception ex)
            {
                return new ServiceResult<TokenDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<TokenDTO>> RegisterServices(RegisterDTO entity)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(entity.Username);
                var userEmailExists = await _userManager.FindByEmailAsync(entity.Email);
                if (userExists != null)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "माफ गर्नु होला, प्रयोकर्ता को नाम पहिले देखि छ" });
                }
                if (userEmailExists != null)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "माफ गर्नु होला, इमेल पहिले देखि प्रयोगमा छ " });

                }

                if ((entity.RoleId != 1 && entity.RoleId != 2))
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "माफ गर्नु होला,रोल भेटिएन" });
                }

                var branchExists = await _dbContext.branches.AnyAsync(x => x.BranchId == entity.BranchId);

                if (!branchExists)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "शाखा भेटिएन" });
                }

                ApplicationUser user = new()
                {
                    Email = entity.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = entity.Username,
                    PhoneNumber = entity.PhoneNumber,
                    BranchId = entity.BranchId,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, entity.Password);
                if (!result.Succeeded)
                {
                    return new ServiceResult<TokenDTO>(false, errors: new[] { "प्रयोगकर्ता सिर्जना असफल भयो ! कृपया प्रयोगकर्ता विवरणहरू जाँच गर्नुहोस् र पुन: प्रयास गर्नुहोस् !" });
                }


                //Assign users to Role User
                if (entity.RoleId == 1)
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    }

                    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }

                }

                if (entity.RoleId == 2)
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                    }
                    if (await _roleManager.RoleExistsAsync(UserRoles.User))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                    }
                }

                return new ServiceResult<TokenDTO>(true);


            }
            catch (Exception ex)
            {
                return new ServiceResult<TokenDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<UserDTO>> ResetPassword(ChangePasswordDTO entity)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(entity.Username);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPassResult = await _userManager.ResetPasswordAsync(user, token, entity.NewPassword);

                    if (!resetPassResult.Succeeded)
                    {
                        return new ServiceResult<UserDTO>(false, errors: resetPassResult.Errors.Select(x => x.Description).ToArray());
                    }

                    return new ServiceResult<UserDTO>(true);
                }
                return new ServiceResult<UserDTO>(false, errors: new[] { "प्रयोगकर्ता  भेटिएन" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<RevokeUserDTO>> RevokeAllUser()
        {
            try
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    user.RefreshToken = null;
                    await _userManager.UpdateAsync(user);
                }


                return new ServiceResult<RevokeUserDTO>(true);

            }
            catch (Exception ex)
            {
                new ServiceResult<RevokeUserDTO>(false, errors: new[] { ex.Message });
                throw;
            }
        }

        public async Task<ServiceResult<RevokeUserDTO>> RevokeUser(string authorization)
        {
            try
            {
                var token = authorization.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                _blackListServices.Blacklist(jwt.Id);


                var username = jwt.Claims.FirstOrDefault(x => x.Type == "username");
                var user = await _userManager.FindByNameAsync(username.Value);
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);

                return new ServiceResult<RevokeUserDTO>(true);

            }
            catch (Exception ex)
            {
                return new ServiceResult<RevokeUserDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<UserDTO>> UpdateUser(UserDTO entity)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(entity.UserId);
                if (user != null)
                {
                    user.IsActive = entity.IsActive;
                    await _userManager.UpdateAsync(user);
                    return new ServiceResult<UserDTO>(true, entity);
                }
                return new ServiceResult<UserDTO>(false, errors: new[] { "प्रयोगकर्ता  भेटिएन" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO>(false, errors: new[] { ex.Message });
            }
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken
                (
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;

        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false,
            };

            var tokenHandeler = new JwtSecurityTokenHandler();
            var principal = tokenHandeler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException("Invalid Token");
            }

            return principal;
        }
    }
}
