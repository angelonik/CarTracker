using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
