using System;

namespace AuctionSystem.ORM
{
    public class City
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Region { get; set; }
              
        public Country Country { get; set; }

    }
}
