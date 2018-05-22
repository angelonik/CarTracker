using AutoMapper.QueryableExtensions;
using DataAccess;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using System.Collections.Generic;
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
            return await _dbContext.Users
                .ProjectTo<UserWithCarsDto>()
                .FirstOrDefaultAsync(usr => usr.Id == id);
        }

        public async Task Add(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
