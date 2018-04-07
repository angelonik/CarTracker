using AutoMapper;
using DomainModel;

namespace Services.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
