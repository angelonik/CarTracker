using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<UserCar> UserCars { get; set; } = new HashSet<UserCar>();
    }
}
