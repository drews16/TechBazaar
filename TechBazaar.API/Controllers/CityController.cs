using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CityController(
        ICityService cityService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCities()
        {
            var response = await cityService
                .GetAllCitiesAsync();

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}