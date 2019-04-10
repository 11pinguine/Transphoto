using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.ORM
{
    public class VehicleHistory
    {
        public int Id { get; set; }
        public String EvidenceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String CarLicensePlate { get; set; }
        public String Podtyp { get; set; }

        public Vehicle vehicle;
        public Depot depot;
  
        public City city;

    }
}
