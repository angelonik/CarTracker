using System;
using DomainModel;
using System.Linq.Expressions;

namespace Services.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public static Expression<Func<Car, CarDto>> Projection()
        {
            return x => new CarDto
            {
                Id = x.Id,
                Model = x.Model,
                Year = x.Year
            };
        }
    }
}
