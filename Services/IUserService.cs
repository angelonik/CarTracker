﻿using DomainModel;
using Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserWithCarsDto> GetUserWithCars(int id);
        Task Add(User user);
        Task<bool> Delete(int id);
        Task<bool> Edit(int id, User userFromRequest);
    }
}
