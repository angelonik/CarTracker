using AutoMapper;
using DomainModel;

namespace Services.Dtos
{
    public class ManufacturerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }

    public class ManufacturerDtoProfile : Profile
    {
        public ManufacturerDtoProfile()
        {
            CreateMap<Manufacturer, ManufacturerDto>();
        }
    }
}
