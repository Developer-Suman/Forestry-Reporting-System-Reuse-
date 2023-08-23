using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reuse.Bll.DTO;
using Reuse.Bll.Service.Interface;

namespace ReuseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthenticateController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LogInDTO model)
        {
            var login = await _authServices.LoginService(model);
            if(!login.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, login.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, login.Data);
        }

        //[Authorize(Policy = "BlacklistedToken", Roles ="SuperAdmin, Admin")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var register = await _authServices.RegisterServices(model);
            if(!register.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, register.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, register.Data);
        }

        [Authorize]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var refreshToken = await _authServices.RefreshTokenServices(tokenModel);
            if(!refreshToken.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, refreshToken.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, refreshToken.Data);
        }

        [Authorize]
        [HttpPost]
        [Route("revokeuser")]
        public async Task<IActionResult> RevokeUser([FromHeader(Name ="Authorization")] string authorization)
        {
            var result=  await _authServices.RevokeUser(authorization);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }


        [Authorize(Policy = "BlacklistedToken", Roles ="SuperAdmin, Admin")]
        [HttpGet]
        [Route("GetUserByBranchId/{Id}")]
        public async Task<IActionResult> GetUserByBranchId(int Id)
        {
            var result = await _authServices.GetUserByBranchId(Id);
            if(!result.Success)
            {
                StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, result.Data);
        }




        [Authorize(Policy = "BlacklistedToken", Roles="SuperAdmin")]
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authServices.GetAllUsers();
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }


        [Authorize(Policy = "BlacklistedToken", Roles ="SuperAdmin, Admin")]
        [HttpPatch]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            var result= await _authServices.UpdateUser(userDTO);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        //[Authorize(Policy = "BlacklistedToken")]
        [HttpPost]
        [Route("ChangePassword")]

        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var result = await _authServices.ChangePassword(changePasswordDTO);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, "तपाईको पासवर्ड परिवर्तन सफलत भयो ।");
        }

        #region Reset Password
        [Authorize(Policy = "BlacklistedToken", Roles="SuperAdmin")]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var result = await _authServices.ChangePassword(changePasswordDTO);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);

            }
            return StatusCode(StatusCodes.Status200OK, "तपाईको पासवर्ड परिवर्तन सफल भयो ।");
        }

        #endregion
    }
}