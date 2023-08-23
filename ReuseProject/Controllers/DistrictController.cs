using Microsoft.AspNetCore.Mvc;
using Reuse.Bll.Service.Interface;

namespace ReuseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : Controller
    {
        private readonly IDistrictServices _districtServices;

        public DistrictController(IDistrictServices districtServices)
        {
            _districtServices = districtServices;
        }
        [HttpGet]
        [Route("GetAllDistricts")]
        public async Task<IActionResult> GetAllDistrict()
        {
            var result = await _districtServices.GetAllDistricts();
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetDistrictByProvianceId/{Id}")]
        public async Task<IActionResult> GetAllDistricts(int Id)
        {
            var result = await _districtServices.GetDistrictByProvianceid(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }
    }
}
