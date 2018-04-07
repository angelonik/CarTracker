using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AirFiTest.Controllers
{
    [Route("api/[controller]")]
    public class ManufacturersController : Controller
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _manufacturerService.GetAllWithCars());
        }
    }
}
