using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace AirFiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAll();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserWithCarsDto>> Get(int id)
        {
            var user = await _userService.GetUserWithCars(id);

            if(user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(User user)
        {
            await _userService.Add(user);

            return CreatedAtAction(nameof(Get), new { user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var deleted = await _userService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return new NoContentResult();            
        }
    }
}
