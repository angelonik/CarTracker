using AutoMapper;
using DomainModel;
using System;
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
    }

    public class CarWithManufacturerDtoProfile : Profile
    {
        public CarWithManufacturerDtoProfile()
        {
            CreateMap<UserCar, CarWithManufacturerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Car.Id))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Car.Model))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Car.Year))
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => Mapper.Map<ManufacturerDto>(src.Car.Manufacturer)));
        }
    }
}
