using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reuse.Bll.DTO;
using Reuse.Bll.Service.Interface;

namespace ReuseProject.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : Controller
    {
        private readonly IBranchServices<BranchDTO> _service;

        public BranchController(IBranchServices<BranchDTO> branchServices)
        {
            _service = branchServices;
        }
        [HttpPost]
        [Route("addBranch")]
        public async Task<IActionResult> AddBranch([FromBody] BranchDTO branchDTO)
        {
            var token = HttpContext.Request.Headers.Authorization;
            var result = await _service.AddBranch(branchDTO, token);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("InitialSetup")]
        public async Task<IActionResult> InitialSetup([FromBody] BranchDTO branchDTO)
        {
            var result = await _service.AddInitialBranch(branchDTO);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status201Created, new
            {
                Username = "superadmin",
                Password = "Admin@123",
                Email = "superadmin@gmail.com"
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("CheckInitilization")]
        public async Task<ActionResult<bool>> CheckInitiallization()
        {
            var result = await _service.CheckInitialization();
            return Ok(result.Success);
        }

        [HttpGet]
        [Route("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches()
        {
            var result = await _service.GetAllBranch();
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetHeadBranches/{ParentBranchId}")]
        public async Task<ActionResult> GetHeadBranches(int ParentBranchId) 
        {
            var result = await _service.GetHeadBranches(ParentBranchId);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetBranchesById/{Id}")]
        public async Task<ActionResult> GetBranchesById(int Id)
        {
            var result = await _service.GetBranchesById(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetBranchById/{Id}")]
        public async Task<ActionResult> GetBranchById(int Id)
        {
            var result = await _service.GetBranchById(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result?.Data);
        }

        [HttpPatch]
        [Route("UpdateBranch")]
        public async Task<ActionResult> UpdateBranch([FromBody] BranchDTO branchDTO)
        {
            var result = await _service.UpdateBranch(branchDTO);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }


        [HttpDelete]
        [Route("DeleteBranch/{Id}")]
        public async Task<ActionResult> DeleteBranch(int Id)
        {
            var result = await _service.DeleteBranch(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK,result.Data);
        }

        [HttpGet]
        [Route("GetHeadBranches")]
        public async Task<IActionResult> GetBranchesToCreateAdmin()
        {
            var result=  await _service.GetBranchesToCreateAdmin();
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetBranchesByBranchType/{Id}")]
        public async Task<ActionResult> GetBranchesByBranchType(int Id)
        {
            var result = await _service.GetBranchesByBranchType(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }
    }
}
