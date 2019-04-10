using System;

namespace AuctionSystem.ORM
{
    public class Country
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Continent { get; set; }
        
        public Country(int id, string name, string continent)
        {
            this.Id = id;
            this.Name = name;
            this.Continent = continent;
        }
        public Country() { }

    }
}
