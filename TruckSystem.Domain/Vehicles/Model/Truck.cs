using System;

namespace TruckSystem.Domain.Vehicles.Model
{
    public class Truck
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ManufactureYear { get; set; }

        public int ModelYear { get; set; }

        public virtual Model Model { get; set; }

        public int IdModel { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
