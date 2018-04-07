using Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerWithCarsDto>> GetAllWithCars();
    }
}
