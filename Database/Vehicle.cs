using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public VehicleModel vehicleModel; // M:1
        //public Collection<VehicleHistory>  vehicleHistory; //1:N
        public Photo photo; //M:N
        public Depot depot;
        public City city;
    }
}
