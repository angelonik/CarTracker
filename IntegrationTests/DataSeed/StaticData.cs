using DomainModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace IntegrationTests.DataSeed
{
    static class StaticData
    {
        static StaticData()
        {
            Users = JsonConvert.DeserializeObject<List<User>>(
                File.ReadAllText("DataSeed" + Path.DirectorySeparatorChar + "Users.json"));
            Manufacturers = JsonConvert.DeserializeObject<List<Manufacturer>>(
                File.ReadAllText("DataSeed" + Path.DirectorySeparatorChar + "Manufacturers.json"));
            Cars = JsonConvert.DeserializeObject<List<Car>>(
                File.ReadAllText("DataSeed" + Path.DirectorySeparatorChar + "Cars.json"));
            UserCars = JsonConvert.DeserializeObject<List<UserCar>>(
                File.ReadAllText("DataSeed" + Path.DirectorySeparatorChar + "UserCars.json"));
        }

        public static List<User> Users { get; }
        public static List<Manufacturer> Manufacturers { get; }
        public static List<Car> Cars { get; }
        public static List<UserCar> UserCars { get; }
    }
}
