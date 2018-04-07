using AutoMapper;
using DomainModel;

namespace Services.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
    }

    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
