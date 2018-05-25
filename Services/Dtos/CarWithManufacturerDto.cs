using System;
using DomainModel;
using System.Linq.Expressions;

namespace Services.Dtos
{
    public class CarWithManufacturerDto
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string PlateNumber { get; set; }

        public ManufacturerDto Manufacturer { get; set; }

        public static Expression<Func<UserCar, CarWithManufacturerDto>> Projection()
        {
            return x => new CarWithManufacturerDto
            {
                Id = x.Car.Id,
                PlateNumber = x.PlateNumber,
                Model = x.Car.Model,
                Year = x.Car.Year,
                Manufacturer = new ManufacturerDto
                {
                    Id = x.Car.Manufacturer.Id,
                    Name = x.Car.Manufacturer.Name,
                    Country = x.Car.Manufacturer.Country
                }
            };
        }
    }
}
