using AutoMapper;
using DomainModel;
using System.Collections.Generic;

namespace Services.Dtos
{
    public class ManufacturerWithCarsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public IEnumerable<CarDto> Cars { get; set; } = new HashSet<CarDto>();
    }

    public class ManufacturerWithCarsDtoMappingProfile : Profile
    {
        public ManufacturerWithCarsDtoMappingProfile()
        {
            CreateMap<Manufacturer, ManufacturerWithCarsDto>();
        }
    }
}
