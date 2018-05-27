using DataAccess;
using DomainModel;
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
                .Select(UserDto.Projection())
                .ToListAsync();
        }

        public async Task<UserWithCarsDto> GetUserWithCars(int id)
        {
            return await _dbContext.Users
                .Select(UserWithCarsDto.Projection())
                .FirstOrDefaultAsync(usr => usr.Id == id);
        }

        public async Task Add(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Edit(int id, User userFromRequest)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return false;
            }

            user.Name = userFromRequest.Name;
            user.Email = userFromRequest.Email;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
