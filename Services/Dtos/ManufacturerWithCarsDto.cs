using System;
using DomainModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Services.Dtos
{
    public class ManufacturerWithCarsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public IEnumerable<CarDto> Cars { get; set; } = new List<CarDto>();

        public static Expression<Func<Manufacturer, ManufacturerWithCarsDto>> Projection()
        {
            return x => new ManufacturerWithCarsDto
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.Country,
                Cars = x.Cars.AsQueryable().Select(CarDto.Projection())
            };
        }
    }
}
