using System;
using DomainModel;
using System.Linq.Expressions;

namespace Services.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public static Expression<Func<User, UserDto>> Projection()
        {
            return x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            };
        }
    }
}
