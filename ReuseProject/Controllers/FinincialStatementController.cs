using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reuse.Bll.DTO;
using Reuse.Bll.Service.Interface;

namespace ReuseProject.Controllers
{
    [Authorize(Policy ="BlackListedToken")]
    [Route("api/[controller]")]
    [ApiController]
    public class FinincialStatementController : Controller
    {
        private readonly IFinancialStatementServices _service;

        public FinincialStatementController(IFinancialStatementServices financialStatementServices)
        {
            _service = financialStatementServices;
            
        }
        [HttpPost]
        [Route("AddFinancialStatement")]
        public async Task<IActionResult> AddFinancialStatement([FromBody] FinancialStatementDTO financialStatementDTO)
        {
            var result = await _service.AddFinancialStatement(financialStatementDTO);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetAllFinancialStatement/{filter}")]
        public async Task<IActionResult> GetAllFinancialStatement(string filter)
        {
            var result = await _service.GetAllFinancialStatement(filter);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpGet]
        [Route("GetFinancialStatementById/{Id}")]
        public async Task<IActionResult> GetFinancialById(int Id)
        {
            var result = await _service.GetFinancialStatementById(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpPatch]
        [Route("UpdateFinancialStatement")]
        public async Task<IActionResult> UpdateFinancialStatement([FromBody] FinancialStatementDTO financialStatementDTO)
        {
            var result = await _service.UpdateFinancialStatement(financialStatementDTO);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return StatusCode(StatusCodes.Status200OK, result.Data);
        }

        [HttpDelete]
        [Route("DeleteFinancialStatement/{Id}")]
        public async Task<IActionResult> DeleteFinancialStatement(int Id)
        {
            var result = await _service.DeleteFinancialStatement(Id);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result?.Errors);

            }
            return StatusCode(StatusCodes.Status200OK, result?.Data);
        }

    }
}
