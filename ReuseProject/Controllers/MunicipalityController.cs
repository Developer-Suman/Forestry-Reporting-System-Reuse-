using Microsoft.AspNetCore.Mvc;
using Reuse.Bll.Service.Interface;

namespace ReuseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityController : Controller
    {
        private readonly IMunicipalityServices _municipalityServices;

        public MunicipalityController(IMunicipalityServices municipalityServices)
        {
            _municipalityServices = municipalityServices;
            
        }


        [HttpGet]
        [Route("GetAllMunicipalities")]
        public async Task<IActionResult> GetAllMunicipalities()
        {
            var result= await _municipalityServices.GetAllMunicipalities();
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetMunicipalityByDistrictId/{Id}")]
        public async Task<IActionResult> GetMunicipalitiesByDistrictId(int Id)
        {
            var result = await _municipalityServices.GetMunicipalityByDistrictId(Id);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
      
        }
    }
}
