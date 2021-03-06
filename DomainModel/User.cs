﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserCar> UserCars { get; set; } = new List<UserCar>();
    }
}
