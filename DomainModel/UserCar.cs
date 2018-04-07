using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class UserCar
    {
        public int Id { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
