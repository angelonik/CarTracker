using System;
using DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Dtos
{
    public class UserWithCarsDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public IEnumerable<CarWithManufacturerDto> Cars { get; set; } =
            new List<CarWithManufacturerDto>();

        public static Expression<Func<User, UserWithCarsDto>> Projection()
        {
            return x => new UserWithCarsDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Cars = x.UserCars.AsQueryable().Select(CarWithManufacturerDto.Projection())
            };
        }
    }
}