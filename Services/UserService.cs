using AutoMapper.QueryableExtensions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _dbContext;

        public UserService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _dbContext.Users
                .ProjectTo<UserDto>()
                .ToListAsync();
        }

        public async Task<UserWithCarsDto> GetUserWithCars(int id)
        {
            //TODO: fix this query had to use a hack because of bug with entity framework preview-1
            return (await _dbContext.Users
                .ProjectTo<UserWithCarsDto>()
                .Where(usr => usr.Id == id)
                .Take(1)
                .ToListAsync())
                .FirstOrDefault();
        }
    }
}
