using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ApplicationContext _dbContext;

        public ManufacturerService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ManufacturerWithCarsDto>> GetAllWithCars()
        {
            return await _dbContext.Manufacturers
                .Select(ManufacturerWithCarsDto.Projection())
                .ToListAsync();
        }
    }
}
