using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.ORM
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public String Manufacturer { get; set; }
        public String Model { get; set; }
        public int CapacityStanding { get; set; }
        public int CapacitySeating { get; set; }
        public String VehicleType { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Weight { get; set; }
        public int MaxSpeed { get; set; }
        public bool LowFloor { get; set; }
        public String PoweredBy { get; set; }



    }
}
