using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class OfficeController(
        IOfficeService officeService) : ControllerBase
    {
        [Authorize]
        [HttpGet("{cityId:int}")]
        public async Task<IActionResult> GetOffices(int cityId)
        {
            var response = await officeService
                .GetByCityId(cityId);

            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}