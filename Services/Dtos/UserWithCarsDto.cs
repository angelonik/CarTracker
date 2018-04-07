using AutoMapper;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace Services.Dtos
{
    public class UserWithCarsDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public IEnumerable<CarWithManufacturerDto> Cars { get; set; } =
            new List<CarWithManufacturerDto>();
    }

    public class UserWithCarsMappingProfile : Profile
    {
        public UserWithCarsMappingProfile()
        {
            CreateMap<User, UserWithCarsDto>()
                .ForMember(dest => dest.Cars, opt => opt.MapFrom(src => src.UserCars));
        }
    }
}