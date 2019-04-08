using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.ORM
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ConstructionYear { get; set; }
        public String State { get; set; }
        public String CarLicensePlate { get; set; }
        public String EvidenceId { get; set; }
        public String MainPhotoPath { get; set; }
        public String Podtyp { get; set; }

        public VehicleModel vehicleModel;
        public VehicleHistory vehicleHistory;
        public Photo photo;
        public City city;


    }
}
