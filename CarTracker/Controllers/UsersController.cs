using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AirFiTest.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserWithCars(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
